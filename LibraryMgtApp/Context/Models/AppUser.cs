using LibraryMgtApp.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryMgtApp.Context.Models
{
    public class AppUser : IdentityUser<Guid>, IValidatableObject
    {
        public AppUser()
        {
            Id = Guid.NewGuid();
            CreatedOnUtc = DateTime.Now.GetDateUtcNow();
        }
        public static AppUser Create(String fname, String lname, String email,
            String gender, String phone, String nin)
        {
            return new AppUser()
            {
                FirstName = fname,
                LastName = lname,
                Email = email,
                Gender = gender,
                PhoneNumber = phone,
                NIN = nin
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (String.IsNullOrEmpty(this.Email))
            {
                yield return new ValidationResult("Email is required!");
            }

            if (String.IsNullOrEmpty(this.PhoneNumber))
            {
                yield return new ValidationResult("PhoneNumber is required!");
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool Activated { get; set; }
        public bool IsDisabled { get; set; }
        public string Gender { get; set; }
        public string NIN { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }

    public class ApplicationIdentityUserClaim : IdentityUserClaim<Guid>
    {

    }

    public class ApplicationIdentityUserLogin : IdentityUserLogin<Guid>
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }

    public class ApplicationIdentityRole : IdentityRole<Guid>
    {
        public ApplicationIdentityRole()
        {
            Id = Guid.NewGuid();
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
    }

    public class ApplicationIdentityUserRole : IdentityUserRole<Guid>
    {

    }

    public class ApplicationIdentityRoleClaim : IdentityRoleClaim<Guid>
    {

    }

    public class ApplicationIdentityUserToken : IdentityUserToken<Guid>
    {

    }
}
