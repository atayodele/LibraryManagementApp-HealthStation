using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMgtApp.Context.Maps
{
    public class ApplicationIdentityRoleMap : IEntityTypeConfiguration<ApplicationIdentityRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationIdentityRole> builder)
        {
            var roles = new ApplicationIdentityRole[]
            {
               new  ApplicationIdentityRole
               {
                   Id = RoleHelpers.ADMIN_ID(),
                    Name=  RoleHelpers.ADMIN,
                    NormalizedName = RoleHelpers.ADMIN

               },
               new  ApplicationIdentityRole
               {
                   Id = RoleHelpers.USER_ID(),
                    Name=  RoleHelpers.USER,
                 NormalizedName = RoleHelpers.USER
               }
            };

            builder.HasData(roles);
        }
    }
}
