using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Extensions
{
    public static class RoleHelpers
    {
        public static Guid ADMIN_ID() => Guid.Parse("5869ab93-81da-419b-b5ad-41a7bc82cae8");
        public const string ADMIN = nameof(ADMIN);
        public static Guid USER_ID() => Guid.Parse("e4410972-f20a-4d07-afdb-c61550e3dd44");
        public const string USER = nameof(USER);

        public static List<string> GetAll()
        {
            return new List<string> { ADMIN, USER };
        }
    }
}
