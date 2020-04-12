using LibraryMgtApp.Context;
using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Dto;
using LibraryMgtApp.Extensions;
using LibraryMgtApp.Extensions.Helpers;
using LibraryMgtApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Infrastructure.Repository
{
    public class CheckoutService : Service<Checkout>, ICheckoutService
    {
        private readonly IBookService _bookServ;
        private readonly IUserMgmtService _userServ;

        public CheckoutService(IBookService bookServ,
            IUserMgmtService userServ,
            IUnitOfWork iuow) : base(iuow)
        {
            _bookServ = bookServ;
            _userServ = userServ;
        }

        public async Task<Checkout> GetCheckoutBook(Guid id)
        {
            var book = await this.GetAllAsync(1, 1, a => a.Id, a => a.Id == id, OrderBy.Ascending);
            var returnBook = book.FirstOrDefault();
            return returnBook;
        }

        public async Task<(List<ValidationResult> Result, CheckoutDto Checkout)> CheckoutBook(CheckoutDto vm)
        {
            results.Clear();
            try
            {
                var user = await _userServ.GetUser(vm.UserId);
                if (user == null)
                {
                    results.Add(new ValidationResult("Invalid User ID."));
                    return (results, null);
                }
                //calculate the elapsed date
                int days = 10;
                var ElapsedDate = DateTime.Now.AddBusinessDays(days);

                var checkout = new Checkout();
                checkout.Id = Guid.NewGuid();
                checkout.UserId = vm.UserId;
                checkout.CheckoutDate = DateTime.Now.GetDateUtcNow();
                checkout.ReturnDate = ElapsedDate;
                checkout.CreatedOn = DateTime.Now.GetDateUtcNow();
                checkout.ModifiedOn = DateTime.Now.GetDateUtcNow();

                this.UnitOfWork.BeginTransaction();

                if (vm.SelectedBooks?.Count > 0)
                {
                    foreach (var userStore in vm.SelectedBooks)
                    {
                        //get details of selected book
                        var checkBook = _bookServ.FirstOrDefault(s => s.Id == userStore.Book_Id);
                        if (checkBook == null)
                        {
                            results.Add(new ValidationResult("Selected Book's are Wrong"));
                            return (results, null);
                        }
                        checkBook.ISBN = checkBook.ISBN;

                        var bookCheckout = new BookCheckout()
                        {
                            Book = checkBook,
                            CheckoutId = checkout.Id,
                            CreatedOn = DateTime.Now.GetDateUtcNow(),
                            ModifiedOn = DateTime.Now.GetDateUtcNow()
                        };

                        checkout.BookCheckouts.Add(bookCheckout);
                    }
                }
                await this.AddAsync(checkout);
                await this.UnitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                results.Add(new ValidationResult($"Unable to Checkout! \n {ex.Message}"));
            }
            return (results, vm);
        }

        public async Task<(List<ValidationResult> Result, CheckoutDto Checkout)> CheckInBook(CheckoutDto vm, Guid checkId)
        {
            results.Clear();
            try
            {
                var checkout = this.GetAll(1, 1, a => a.Id, a => a.Id == checkId, OrderBy.Descending).FirstOrDefault();
                if (checkout == null || checkout.Id != checkId)
                {
                    results.Add(new ValidationResult("Invalid Checkout ID."));
                    return (results, null);
                }

                var user = await _userServ.GetUser(vm.UserId);
                if (user == null)
                {
                    results.Add(new ValidationResult("Invalid User ID."));
                    return (results, null);
                }
                decimal pay = 0;
                if (DateTime.Today > checkout.ReturnDate)
                {
                    pay += 200;
                }
                else
                {
                    pay += 0;
                }

                if (checkout.BookCheckouts.Any())
                {
                    checkout.BookCheckouts.Clear();
                }

                if (vm.SelectedBooks?.Count > 0)
                {
                    foreach (var userStore in vm.SelectedBooks)
                    {
                        //get details of selected book
                        var checkBook = _bookServ.FirstOrDefault(s => s.Id == userStore.Book_Id);
                        if (checkBook == null)
                        {
                            results.Add(new ValidationResult("Selected Book's are Wrong"));
                            return (results, null);
                        }
                        checkBook.ISBN = checkBook.ISBN;

                        var bookCheckout = new BookCheckout()
                        {
                            Book = checkBook,
                            CheckoutId = checkout.Id,
                            CreatedOn = DateTime.Now.GetDateUtcNow(),
                            ModifiedOn = DateTime.Now.GetDateUtcNow()
                        };

                        checkout.BookCheckouts.Add(bookCheckout);
                    }
                }
                checkout.ModifiedOn = DateTime.Now.GetDateUtcNow();
                checkout.CheckInDate = DateTime.Now.GetDateUtcNow();
                checkout.OverDueAmount = pay;

                this.UnitOfWork.BeginTransaction();
                await this.UpdateAsync(checkout);
                await this.UnitOfWork.CommitAsync();
                return (results, vm);
            }
            catch (Exception ex)
            {
                results.Add(new ValidationResult($"Unable to Checkout! \n {ex.Message}"));
            }
            return (results, vm);
        }
    }
}
