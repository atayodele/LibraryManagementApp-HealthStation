using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Context.Models
{
    public class Checkout : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public decimal? OverDueAmount { get; set; }
        public ICollection<BookCheckout> BookCheckouts { get; set; }

        public Checkout()
        {
            BookCheckouts = new List<BookCheckout>();
        }
    }
}
