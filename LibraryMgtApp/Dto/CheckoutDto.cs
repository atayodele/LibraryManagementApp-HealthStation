using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Dto
{
    public class CheckoutDto
    {
        public Guid UserId { get; set; }
        public List<BookViewModel> SelectedBooks { get; set; }
    }
}
