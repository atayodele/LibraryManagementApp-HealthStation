using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMgtApp.Context.Maps
{
    public class ApplicationIdentityUserRoleMap : IEntityTypeConfiguration<ApplicationIdentityUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationIdentityUserRole> builder)
        {
            builder.HasKey(p => new { p.UserId, p.RoleId });

            var users_roles = new[]
            {
                 new
                {
                    UserId = DefaultUserKeys.AdminUserId,
                    RoleId = RoleHelpers.ADMIN_ID(),
                },
                 new
                {
                    UserId = DefaultUserKeys.UserId,
                    RoleId = RoleHelpers.USER_ID()
                }
            };

            //builder.HasKey(model => new { model.UserId, model.RoleId });
            builder.HasData(users_roles);
        }
    }
}
