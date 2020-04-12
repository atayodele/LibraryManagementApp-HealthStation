using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Extensions
{
    public static class DefaultUserKeys
    {

        public static readonly Guid AdminUserId = Guid.Parse("129712e3-9214-4dd3-9c03-cfc4eb9ba979");
        public const string AdminMobile = "07032369247";
        public const string AdminUserEmail = "admin@gmail.com";

        public static readonly Guid UserId = Guid.Parse("193a9488-ad75-41d6-a3e0-db3f10b6468f");
        public const string UserMobile = "07032367234";
        public const string UserEmail = "user@gmail.com";
    }
}
