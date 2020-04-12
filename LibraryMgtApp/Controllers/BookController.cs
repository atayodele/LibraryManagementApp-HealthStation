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
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserMgmtService _userSrv;
        private readonly IAuthorService _authorServ;
        private readonly IBookService _bookServ;

        public BookController(
            IMapper mapper,
            IUserMgmtService userSrv,
            IAuthorService authorServ,
            IBookService bookServ)
        {
            _mapper = mapper;
            _userSrv = userSrv;
            _authorServ = authorServ;
            _bookServ = bookServ;
        }

        [HttpGet(nameof(GetBooks))]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> GetBooks(UserParams userParams)
        {
            var book = await _bookServ.GetBooks(userParams);
            var bookToReturn = _mapper.Map<IEnumerable<BookForListDto>>(book);
            Response.AddPagination(book.CurrentPage, book.PageSize, book.TotalCount, book.TotalPages);
            return Ok(bookToReturn);
        }

        [HttpGet]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("invalid id");
                }
                var book = await _bookServ.GetBookById(id);

                if (book == null)
                {
                    return BadRequest("Book doesn't exist.");
                }
                var author = await _authorServ.GetAuthorById(book.AuthorId);

                var bookViewModel = new BookForDetailsDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    ISBN = book.ISBN,
                    PublichYear = book.PublichYear,
                    Cost = book.Cost,
                    Status = book.Status,
                    AuthorId = book.AuthorId,
                    AuthorName = author.AuthorName
                };
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(Create))]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Create([FromBody]AddBookDto payload)
        {
            ApiResponse<AddBookDto> response = new ApiResponse<AddBookDto>();
            try
            {
                if (!response.Errors.Any())
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);
                    (List<ValidationResult> Result, AddBookDto Book) errorResult = await _bookServ.CreateBook(payload);

                    if (errorResult.Result.Any())
                    {
                        return BadRequest(new { errorList = $"{errorResult.Result.FirstOrDefault().ErrorMessage}" });
                    }
                    else
                    {
                        response.Code = ApiResponseCodes.OK;
                        response.Description = $"Book creation successful.";
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpPut("{id}/update/{BookId}")]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Update([FromBody]UpdateBookDto payload, Guid id, Guid BookId)
        {
            ApiResponse<UpdateBookDto> response = new ApiResponse<UpdateBookDto>();
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
                    (List<ValidationResult> Result, UpdateBookDto Book) errorResult = await _bookServ.UpdateBook(payload, BookId);
                    if (errorResult.Result.Any())
                    {
                        return BadRequest(new { errorList = $"{errorResult.Result.FirstOrDefault().ErrorMessage}" });
                    }
                    else
                    {
                        response.Code = ApiResponseCodes.OK;
                        response.Description = $"Book Updated Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}/Delete/{BookId}")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Delete(Guid id, Guid BookId)
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
                    var result = await _bookServ.DeleteBook(BookId);

                    response.Code = ApiResponseCodes.OK;
                    response.Description = $"Book Deleted Successfully.";
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