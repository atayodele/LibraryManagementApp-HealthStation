using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Dto;
using LibraryMgtApp.Extensions.Helpers;
using LibraryMgtApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgtApp.Controllers
{
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutServ;
        private readonly IUserMgmtService _userSrv;
        private readonly IBookService _bookServ;
        private readonly IBookCheckoutService _bookCheckoutServ;

        public CheckoutController(
            ICheckoutService checkoutServ,
            IUserMgmtService userSrv,
            IBookService bookServ,
            IBookCheckoutService bookCheckoutServ)
        {
            _checkoutServ = checkoutServ;
            _userSrv = userSrv;
            _bookServ = bookServ;
            _bookCheckoutServ = bookCheckoutServ;
        }

        [HttpPost("{id}/Checkout")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Checkout([FromBody]CheckoutDto payload, Guid id)
        {

            ApiResponse<CheckoutDto> response = new ApiResponse<CheckoutDto>();
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
                    (List<ValidationResult> Result, CheckoutDto Checkout) errorResult = await _checkoutServ.CheckoutBook(payload);

                    if (errorResult.Result.Any())
                    {
                        return BadRequest(new { errorList = $"{errorResult.Result.FirstOrDefault().ErrorMessage}" });
                    }
                    else
                    {
                        response.Code = ApiResponseCodes.OK;
                        response.Description = $"Checkout successful.";
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPost("{id}/CheckIn/{checkId}")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> CheckIn([FromBody]CheckoutDto payload, Guid id, Guid checkId)
        {

            ApiResponse<CheckoutDto> response = new ApiResponse<CheckoutDto>();
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
                    (List<ValidationResult> Result, CheckoutDto Checkout) errorResult = await _checkoutServ.CheckInBook(payload, checkId);

                    if (errorResult.Result.Any())
                    {
                        return BadRequest(new { errorList = $"{errorResult.Result.FirstOrDefault().ErrorMessage}" });
                    }
                    else
                    {
                        response.Code = ApiResponseCodes.OK;
                        response.Description = $"Checkout successful.";
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }
        [HttpGet]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> GetCheckoutBook(Guid id) 
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("invalid id");
                }
                var check = await _checkoutServ.GetCheckoutBook(id);

                if (check == null)
                {
                    return BadRequest("Invalid Checkout ID");
                }
                var user = await _userSrv.GetUser(check.UserId);
                var bookcheck = _bookCheckoutServ.FirstOrDefault(s => s.Id == check.Id);
                var book = _bookServ.FirstOrDefault(s => s.Id == bookcheck.BookId);

                var bookViewModel = new CheckoutModel()
                {
                    Id = check.Id,
                    UserId = user.Id,
                    Fullname = user.FirstName + " " + user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    NIN = user.NIN,
                    CheckoutDate = check.CheckoutDate,
                    ReturnDate = check.ReturnDate
                };
                return Ok(bookViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}