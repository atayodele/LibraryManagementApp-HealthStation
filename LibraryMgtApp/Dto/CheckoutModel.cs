using LibraryMgtApp.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Dto
{
    public class CheckoutModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string NIN { get; set; }
        public ICollection<Book> Books { get; set; } 
    }
}
