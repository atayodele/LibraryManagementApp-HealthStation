using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Context.Models
{
    public class Author : BaseEntity, IValidatableObject
    {

        public static Author Create(String name)
        {
            return new Author()
            {
                AuthorName = name
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (String.IsNullOrEmpty(this.AuthorName))
            {
                yield return new ValidationResult("Author Name is required!");
            }
        }
        public string AuthorName { get; set; }
    }
}
