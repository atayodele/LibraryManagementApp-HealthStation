using LibraryMgtApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LibraryMgtApp.Context.Models.AppUser;

namespace LibraryMgtApp.Context.Maps
{
    public class ApplicationIdentityUserLoginMap : IEntityTypeConfiguration<ApplicationIdentityUserLogin>
    {
        public void Configure(EntityTypeBuilder<ApplicationIdentityUserLogin> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasKey(u => new { u.LoginProvider, u.ProviderKey });

        }
    }
}
