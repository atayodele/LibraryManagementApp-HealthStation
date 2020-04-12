using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Infrastructure.Interfaces
{
    public interface ICheckoutService : IService<Checkout>
    {
        Task<Checkout> GetCheckoutBook(Guid id); 
        Task<(List<ValidationResult> Result, CheckoutDto Checkout)> CheckoutBook(CheckoutDto vm);
        Task<(List<ValidationResult> Result, CheckoutDto Checkout)> CheckInBook(CheckoutDto vm, Guid checkId); 
    }
}
