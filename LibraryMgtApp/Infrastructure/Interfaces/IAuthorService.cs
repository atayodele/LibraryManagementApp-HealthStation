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
    public interface IAuthorService : IService<Author>
    {
        Task<Author> GetAuthorById(Guid id);
        Task<PagedList<Author>> GetAuthors(UserParams userParams);
        Task<(List<ValidationResult> Result, AddAuthorDto Author)> CreateAuthor(AddAuthorDto vm);
        Task<(List<ValidationResult> Result, UpdateAuthorDto Author)> UpdateAuthor(UpdateAuthorDto vm, Guid author_Id);
        List<Author> GetAuthorList();
        Task<Author> DeleteAuthor(Guid Id);
    }
}
