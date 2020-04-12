using LibraryMgtApp.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMgtApp.Context.Maps
{
    public class ApplicationIdentityUserTokenMap : IEntityTypeConfiguration<ApplicationIdentityUserToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationIdentityUserToken> builder)
        {
            builder.HasKey(p => p.UserId);
        }
    }
}
