using LibraryMgtApp.Context.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Dto
{
    public class AddBookDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public DateTime PublichYear { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        public StatusModes StatusMode { get; set; } 
    }
}
