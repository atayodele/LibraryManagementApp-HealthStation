using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Context.Models
{
    public class BookCheckout : BaseEntity
    {
        public Guid CheckoutId { get; set; }
        [ForeignKey("CheckoutId")]
        public Checkout Checkout { get; set; } 
        public Guid BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
