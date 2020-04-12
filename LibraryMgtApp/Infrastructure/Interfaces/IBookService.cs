using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Dto;
using LibraryMgtApp.Extensions.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Infrastructure.Interfaces
{
    public interface IBookService : IService<Book>
    {
        Task<Book> GetBookById(Guid id);
        Task<PagedList<Book>> GetBooks(UserParams userParams); 
        Task<(List<ValidationResult> Result, AddBookDto Book)> CreateBook(AddBookDto vm);
        Task<(List<ValidationResult> Result, UpdateBookDto Book)> UpdateBook(UpdateBookDto vm, Guid author_Id);
        List<Book> GetBookList();
        Task<Book> DeleteBook(Guid Id);
    }
}
