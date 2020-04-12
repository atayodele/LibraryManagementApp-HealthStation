using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Context.Models
{
    public class Book : BaseEntity, IValidatableObject
    {
        public static Book Create(String title, String isbn)
        {
            return new Book()
            {
                Title = title,
                ISBN = isbn
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (String.IsNullOrEmpty(this.Title))
            {
                yield return new ValidationResult("Title is required!");
            }

            if (String.IsNullOrEmpty(this.ISBN))
            {
                yield return new ValidationResult("ISBN is required!");
            }
        }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public decimal Cost { get; set; }
        public DateTime PublichYear { get; set; }
        public bool Status { get; set; }
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }

        public ICollection<BookCheckout> BookCheckouts { get; set; }
    }
}
