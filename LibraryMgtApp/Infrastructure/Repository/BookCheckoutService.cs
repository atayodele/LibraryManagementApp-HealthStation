using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Infrastructure.Repository
{
    public class BookCheckoutService : Service<BookCheckout>, IBookCheckoutService
    {
        public BookCheckoutService(IUnitOfWork iuow) : base(iuow)
        {

        }
        public async Task<BookCheckout> GetCheckoutBookById(Guid id)
        {
            var book = await this.GetAllAsync(1, 1, a => a.Id, a => a.Id == id, OrderBy.Ascending);
            var returnBook = book.FirstOrDefault();
            return returnBook;
        }
    }
}
