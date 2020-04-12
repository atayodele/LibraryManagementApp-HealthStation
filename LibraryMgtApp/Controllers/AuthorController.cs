using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LibraryMgtApp.Dto;
using LibraryMgtApp.Extensions.Helpers;
using LibraryMgtApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgtApp.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserMgmtService _userSrv;
        private readonly IAuthorService _authorServ;

        public AuthorController(
            IMapper mapper,
            IUserMgmtService userSrv, 
            IAuthorService authorServ)
        {
            _mapper = mapper;
            _userSrv = userSrv;
            _authorServ = authorServ;
        }


        [HttpGet(nameof(GetAuthors))]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> GetAuthors(UserParams userParams)
        {
            var users = await _authorServ.GetAuthors(userParams);
            var userToReturn = _mapper.Map<IEnumerable<AuthorListDto>>(users);
            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(userToReturn);
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> GetAuthor(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("invalid id");
                }
                var author = await _authorServ.GetAuthorById(id);

                if (author == null)
                {
                    return BadRequest("Author doesn't exist.");
                }

                var userViewModel = new AuthorListDto()
                {
                    Id = author.Id,
                    AuthorName = author.AuthorName,
                    CreatedOn = author.CreatedOn,
                    ModifiedOn = author.ModifiedOn
                };
                return Ok(userViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(Create))]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create([FromBody]AddAuthorDto payload)
        {
            ApiResponse<AddAuthorDto> response = new ApiResponse<AddAuthorDto>();
            try
            {
                if (!response.Errors.Any())
                {
                    if (string.IsNullOrEmpty(payload.Name))
                    {
                        return BadRequest(new { errorList = "Author Name is required." });
                    }
                    (List<ValidationResult> Result, AddAuthorDto Author) errorResult = await _authorServ.CreateAuthor(payload);

                    if (errorResult.Result.Any())
                    {
                        return BadRequest(new { errorList = $"{errorResult.Result.FirstOrDefault().ErrorMessage}" });
                    }
                    else
                    {
                        response.Code = ApiResponseCodes.OK;
                        response.Description = $"Author creation successful.";
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("{id}/Update/{AuthorId}")]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Update([FromBody]UpdateAuthorDto payload, Guid id, Guid AuthorId)
        {
            ApiResponse<UpdateAuthorDto> response = new ApiResponse<UpdateAuthorDto>();
            try
            {
                if (!response.Errors.Any())
                {
                    var userFromRepo = await _userSrv.GetUser(id);
                    if (userFromRepo == null)
                    {
                        return BadRequest(new { errorList = "Invalid Author Id" });
                    }
                    var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (currentUserId != userFromRepo.Email)
                    {
                        return BadRequest(new { errorList = "UnAuthorized" });
                    }
                    (List<ValidationResult> Result, UpdateAuthorDto Author) errorResult = await _authorServ.UpdateAuthor(payload, AuthorId);
                    if (errorResult.Result.Any())
                    {
                        return BadRequest(new { errorList = $"{errorResult.Result.FirstOrDefault().ErrorMessage}" });
                    }
                    else
                    {
                        response.Code = ApiResponseCodes.OK;
                        response.Description = $"Author Updated Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}/Delete/{AuthorId}")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Delete(Guid id, Guid AuthorId)
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
                    var result = await _authorServ.DeleteAuthor(AuthorId);

                    response.Code = ApiResponseCodes.OK;
                    response.Description = $"Author Deleted Successfully.";
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