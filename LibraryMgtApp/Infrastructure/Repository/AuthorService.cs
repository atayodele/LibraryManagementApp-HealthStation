using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Dto;
using LibraryMgtApp.Extensions;
using LibraryMgtApp.Extensions.Helpers;
using LibraryMgtApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Infrastructure.Repository
{
    public class AuthorService : Service<Author>, IAuthorService
    {
        public AuthorService(IUnitOfWork iuow) : base(iuow)
        {
        }

        public async Task<(List<ValidationResult> Result, AddAuthorDto Author)> CreateAuthor(AddAuthorDto vm)
        {
            results.Clear();
            try
            {
                var author = Author.Create(vm.Name);

                bool isValid = Validator.TryValidateObject(author, new ValidationContext(author, null, null),
                    results, false);

                if (!isValid || results.Count > 0)
                    return (results, null);
                if (Exist(vm.Name))
                {
                    results.Add(new ValidationResult($"{vm.Name} already exists."));
                    return (results, null);
                }
                author.IsDeleted = false;
                author.CreatedOn = DateTime.Now.GetDateUtcNow();
                author.ModifiedOn = DateTime.Now.GetDateUtcNow();

                await this.AddAsync(author);
            }
            catch (Exception ex)
            {
                results.Add(new ValidationResult($"Author couldn't be created! \n {ex.Message}"));
            }
            return (results, vm);
        }


        private bool Exist(string name)
        {
            Func<Author, bool> predicate = (s) =>
            {
                if (string.IsNullOrEmpty(s.AuthorName))
                    return false;

                var test = s.AuthorName.Equals(name, StringComparison.InvariantCultureIgnoreCase) && s.IsDeleted == false;
                return test;
            };
            return this.FirstOrDefault(predicate) != null;
        }

        public async Task<Author> DeleteAuthor(Guid Id)
        {
            try
            {
                var author = this.FirstOrDefault(p => p.Id == Id);
                if (author == null)
                    return null;
                this.UnitOfWork.BeginTransaction();
                author.IsDeleted = !author.IsDeleted;
                author.ModifiedOn = DateTime.Now.GetDateUtcNow();
                await this.UpdateAsync(author);
                await this.UnitOfWork.CommitAsync();

                return author;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Author> GetAuthorById(Guid id)
        {
            var author = await this.GetAllAsync(1, 1, s => s.Id,
               s => s.Id == id, OrderBy.Ascending);
            var result = author.FirstOrDefault();
            return result;
        }

        public List<Author> GetAuthorList()
        {
            var author = this.GetAll(1, 1, c => c.Id, c => c.IsDeleted == false
            , OrderBy.Ascending);

            return author.ToList();
        }

        public async Task<PagedList<Author>> GetAuthors(UserParams userParams)
        {
            var author = this.GetAll().OrderBy(u => u.Id).Where(x => x.IsDeleted == false);
            return await PagedList<Author>.CreateAsync(author, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<(List<ValidationResult> Result, UpdateAuthorDto Author)> UpdateAuthor(UpdateAuthorDto vm, Guid author_Id)
        {
            results.Clear();
            try
            {
                var author = this.GetAll(1, 1, a => a.Id, a => a.Id == author_Id, OrderBy.Descending).FirstOrDefault();
                if (author == null)
                {
                    results.Add(new ValidationResult("Author couldn't be found to complete update operation."));
                    return (results, null);
                }
                author.AuthorName = vm.Name;
                author.IsDeleted = false;

                bool isValid = Validator.TryValidateObject(author, new ValidationContext(author, null, null),
                    results, false);

                if (!isValid || results.Count > 0)
                    return (results, null);

                author.ModifiedOn = DateTime.Now.GetDateUtcNow();

                this.UnitOfWork.BeginTransaction();
                await this.UpdateAsync(author);
                await this.UnitOfWork.CommitAsync();

                return (results, vm);
            }
            catch (Exception ex)
            {
                results.Add(new ValidationResult($"Author couldn't be updated! \n {ex.Message}"));
            }
            return (results, vm);
        }
    }
}
