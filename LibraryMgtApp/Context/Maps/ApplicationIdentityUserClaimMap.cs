using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LibraryMgtApp.Context.Maps
{
    public class ApplicationIdentityUserClaimMap : IEntityTypeConfiguration<ApplicationIdentityUserClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationIdentityUserClaim> builder)
        {
            builder.HasKey(c => c.Id);
            var iposbiPermissions = (Permission[])Enum.GetValues(typeof(Permission));

            int count = 1;
            foreach (var item in iposbiPermissions)
            {
                switch (item.GetPermissionCategory())
                {
                    case (RoleHelpers.ADMIN):
                        {
                            builder.HasData(new ApplicationIdentityUserClaim()
                            {
                                Id = count++,
                                UserId = DefaultUserKeys.AdminUserId,
                                ClaimType = item.ToString(),
                                ClaimValue = ((int)item).ToString()
                            });
                            break; ;
                        }

                    case (RoleHelpers.USER):
                        {
                            builder.HasData(new ApplicationIdentityUserClaim()
                            {
                                Id = count++,
                                UserId = DefaultUserKeys.UserId,
                                ClaimType = item.ToString(),
                                ClaimValue = ((int)item).ToString()
                            });
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
