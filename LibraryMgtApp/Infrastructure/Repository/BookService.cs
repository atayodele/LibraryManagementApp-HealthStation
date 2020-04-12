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
    public class BookService : Service<Book>, IBookService
    {
        private readonly IAuthorService _authorServ;
        public BookService(
            IAuthorService authorServ,
            IUnitOfWork iuow) : base(iuow)
        {
            _authorServ = authorServ;
        }

        public async Task<(List<ValidationResult> Result, AddBookDto Book)> CreateBook(AddBookDto vm)
        {
            results.Clear();
            try
            {
                var authorId = await _authorServ.GetAuthorById(vm.AuthorId);
                if (authorId == null)
                {
                    results.Add(new ValidationResult("Invalid Author ID."));
                    return (results, null);
                }
                //then book ferry
                var book = Book.Create(vm.Title, vm.ISBN);

                bool isValid = Validator.TryValidateObject(book, new ValidationContext(book, null, null),
                    results, false);

                if (!isValid || results.Count > 0)
                    return (results, null);
                if (Exist(vm.Title, vm.ISBN))
                {
                    results.Add(new ValidationResult($"{vm.Title} with {vm.ISBN} already exists."));
                    return (results, null);
                }
                book.Cost = vm.Cost;
                book.IsDeleted = false;
                book.PublichYear = vm.PublichYear;
                book.AuthorId = vm.AuthorId;
                book.Status = false;
                book.ModifiedOn = book.CreatedOn = DateTime.Now.GetDateUtcNow();

                this.UnitOfWork.BeginTransaction();
                await this.AddAsync(book);
                await this.UnitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                results.Add(new ValidationResult($"Book couldn't be created! \n {ex.Message}"));
            }
            return (results, vm);
        }

        public Task<Book> DeleteBook(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> GetBookById(Guid id)
        {
            var book = await this.GetAllAsync(1, 1, a => a.Id, a => a.Id == id, OrderBy.Ascending);
            var returnBook = book.FirstOrDefault();
            return returnBook;
        }

        public async Task<PagedList<Book>> GetBooks(UserParams userParams)
        {
            var book = this.GetAll().OrderBy(u => u.Id).Where(x => x.IsDeleted == false);
            return await PagedList<Book>.CreateAsync(book, userParams.PageNumber, userParams.PageSize);
        }

        public List<Book> GetBookList()
        {
            var book = this.GetAll(1, 1, c => c.Id, c => c.IsDeleted == false
            , OrderBy.Ascending);
             
            return book.ToList();
        }

        public async Task<(List<ValidationResult> Result, UpdateBookDto Book)> UpdateBook(UpdateBookDto vm, Guid author_Id)
        {
            results.Clear();

            var book = this.GetAll(1, 1, a => a.Id, a => a.Id == vm.Id && a.Title == vm.Title, OrderBy.Ascending).FirstOrDefault();

            if (book == null || book.ISBN != vm.ISBN)
            {
                results.Add(new ValidationResult("Booking couldn't be found to complete update operation."));
                return (results, null);
            }
            book.Cost = vm.Cost;
            book.IsDeleted = false;
            book.PublichYear = vm.PublichYear;
            book.AuthorId = vm.AuthorId;
            book.Status = false;

            bool isValid = Validator.TryValidateObject(book, new ValidationContext(book, null, null),
                results, false);

            if (!isValid || results.Count > 0)
                return (results, null);

            book.ModifiedOn = DateTime.Now.GetDateUtcNow();

            this.UnitOfWork.BeginTransaction();
            await this.UpdateAsync(book);
            await this.UnitOfWork.CommitAsync();
            return (results, vm);
        }


        private bool Exist(string title, string Isbn)
        {
            Func<Book, bool> predicate = (s) =>
            {
                if (string.IsNullOrEmpty(s.Title))
                    return false;
                if (string.IsNullOrEmpty(s.ISBN))
                    return false;

                var test = s.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase) && s.ISBN.Equals(Isbn, StringComparison.InvariantCultureIgnoreCase) && s.IsDeleted == false;
                return test;
            };
            return this.FirstOrDefault(predicate) != null;
        }
    }
}
