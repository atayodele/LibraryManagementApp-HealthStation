using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Extensions
{
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        public IEnumerable<Permission> RequiredPermissions { get; }

        public PermissionsAuthorizationRequirement(IEnumerable<Permission> requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }
    }
}
