using LibraryMgtApp.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Infrastructure.Interfaces
{
    public interface IBookCheckoutService : IService<BookCheckout>
    {
        Task<BookCheckout> GetCheckoutBookById(Guid id);
    }
}
