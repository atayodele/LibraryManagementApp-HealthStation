using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Dto;
using LibraryMgtApp.Extensions.Helpers;
using LibraryMgtApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgtApp.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserMgmtService _userSrv;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(
            IMapper mapper,
            IUserMgmtService userSrv,
            UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userSrv = userSrv;
            _userManager = userManager;
        }

        [HttpGet(nameof(GetUsers))]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> GetUsers(UserParams userParams)
        {
            var users = await _userSrv.GetUsers(userParams);
            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(userToReturn);
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("invalid id");
                }
                var user = await _userManager.FindByIdAsync(id.ToString());

                if (user == null)
                {
                    return BadRequest("User doesn't exist.");
                }

                var userViewModel = new UserForDetailedDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName,
                    Email = user.Email,
                    NIN = user.NIN,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.Gender,
                    CreatedOnUtc = user.CreatedOnUtc
                };
                return Ok(userViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(AddUser))]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> AddUser([FromBody]AddUserDto payload)
        {
            ApiResponse<AddUserDto> response = new ApiResponse<AddUserDto>();
            try
            {
                if (!response.Errors.Any())
                {
                    if (string.IsNullOrEmpty(payload.Password) || string.IsNullOrEmpty(payload.RTPassword))
                    {
                        return BadRequest(new { errorList = "Password & Repeat password is required." });
                    }
                    if (payload.Password != payload.RTPassword)
                    {
                        return BadRequest(new { errorList = "Password & Repeat password does not match." });
                    }
                    if (await _userSrv.UserExists(payload.Email))
                    {
                        return BadRequest(new { errorList = "Email is already taken" });
                    }
                    if (await _userSrv.PhoneExists(payload.PhoneNumber))
                    {
                        return BadRequest(new { errorList = "Phone Number is already taken" });
                    }
                    (List<ValidationResult> Result, AddUserDto User) errorResult = await _userSrv.CreateUser(payload);

                    if (errorResult.Result.Any())
                    {
                        return BadRequest(new { errorList = $"{errorResult.Result.FirstOrDefault().ErrorMessage}" });
                    }
                    else
                    {
                        response.Code = ApiResponseCodes.OK;
                        response.Description = $"User creation successful.";
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("{id}/updateUser/{UserId}")]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> UpdateUser([FromBody]UserForUpdateDto payload, Guid id, Guid UserId)
        {
            ApiResponse<UserForUpdateDto> response = new ApiResponse<UserForUpdateDto>();
            try
            {
                if (!response.Errors.Any())
                {
                    var userFromRepo = await _userSrv.GetUser(id);
                    if (userFromRepo == null)
                    {
                        return BadRequest(new { errorList = "Invalid User Id" });
                    }
                    var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (currentUserId != userFromRepo.Email)
                    {
                        return BadRequest(new { errorList = "UnAuthorized" });
                    }
                    (List<ValidationResult> Result, UserForUpdateDto User) errorResult = await _userSrv.UpdateUser(payload, UserId);
                    if (errorResult.Result.Any())
                    {
                        return BadRequest(new { errorList = $"{errorResult.Result.FirstOrDefault().ErrorMessage}" });
                    }
                    else
                    {
                        response.Code = ApiResponseCodes.OK;
                        response.Description = $"User Updated Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}/DeleteUser/{UserId}")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> DeleteUser(Guid id, Guid UserId)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                if (!response.Errors.Any())
                {
                    var userFromRepo = await _userSrv.GetUser(id);
                    if (userFromRepo == null)
                    {
                        response.Code = ApiResponseCodes.NOT_FOUND;
                        response.Description = $"Invalid User Id";
                    }
                    var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (currentUserId != userFromRepo.Email)
                    {
                        response.Code = ApiResponseCodes.UNAUTHORIZED;
                        response.Description = $"UnAuthorized";
                    }

                    (List<ValidationResult> Result, AppUser User) errorResult = await _userSrv.DeleteUser(UserId);
                    if (errorResult.Result.Any())
                    {
                        response.Description = errorResult.Result.FirstOrDefault().ErrorMessage;
                        response.Code = ApiResponseCodes.ERROR;
                    }
                    else
                    {
                        response.Code = ApiResponseCodes.OK;
                        response.Description = $"User Deleted Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }
    }
}