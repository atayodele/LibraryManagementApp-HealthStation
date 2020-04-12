using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Context.Maps
{
    public class ApplicationIdentityUserMap : IEntityTypeConfiguration<AppUser>
    {
        public ApplicationIdentityUserMap()
        {
        }

        public PasswordHasher<AppUser> Hasher { get; set; }
        = new PasswordHasher<AppUser>();
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AppUser> builder)
        {
            SetupAdmin(builder);
            SetupPowerUser(builder);
        }

        private void SetupAdmin(EntityTypeBuilder<AppUser> builder)
        {
            var adminUser = new AppUser
            {
                Activated = true,
                CreatedOnUtc = DateTime.Now,
                FullName = "Timilehin Ayodele",
                FirstName = "Timilehin",
                LastName = "Ayodele",
                Id = DefaultUserKeys.AdminUserId,
                NIN = "73279-40572",
                Email = DefaultUserKeys.AdminUserEmail,
                EmailConfirmed = true,
                NormalizedEmail = DefaultUserKeys.AdminUserEmail.ToUpper(),
                PhoneNumber = DefaultUserKeys.AdminMobile,
                UserName = DefaultUserKeys.AdminUserEmail,
                NormalizedUserName = DefaultUserKeys.AdminUserEmail.ToUpper(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                PasswordHash = Hasher.HashPassword(null, "micr0s0ft_"),
                SecurityStamp = "953d3fd1-99e3-4fe7-a20d-3598baa96099"
            };

            builder.HasData(adminUser);
        }

        private void SetupPowerUser(EntityTypeBuilder<AppUser> builder)
        {
            var powerUser = new AppUser
            {
                Activated = true,
                CreatedOnUtc = DateTime.Now,
                FullName = "Ayeni Ayodele",
                FirstName = "Ayeni",
                LastName = "Ayodele",
                Id = DefaultUserKeys.UserId,
                NIN = "67354-46571",
                Email = DefaultUserKeys.UserEmail,
                EmailConfirmed = true,
                NormalizedEmail = DefaultUserKeys.UserEmail.ToUpper(),
                PhoneNumber = DefaultUserKeys.UserMobile,
                UserName = DefaultUserKeys.UserEmail,
                NormalizedUserName = DefaultUserKeys.UserEmail.ToUpper(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                PasswordHash = Hasher.HashPassword(null, "micr0s0ft_"),
                SecurityStamp = "9c41c8cc-b489-40c6-bbcf-12edee681919"
            };

            builder.HasData(powerUser);
        }
    }
}
