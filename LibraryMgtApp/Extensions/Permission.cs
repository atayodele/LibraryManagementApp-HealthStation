using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Extensions
{
    public enum Permission
    {
        [Category(RoleHelpers.ADMIN), Description(@"Shall create users")]
        AU_01 = 201,
        [Category(RoleHelpers.ADMIN), Description(@"Shall assign user roles")]
        AU_02 = 202,
        [Category(RoleHelpers.ADMIN), Description(@"Shall create/renew subscriptions")]
        AU_03 = 203,
        [Category(RoleHelpers.ADMIN), Description(@"Shall create and edit stores")]
        AU_04 = 204,
        [Category(RoleHelpers.ADMIN), Description(@"Shall set parameters for email notifications")]
        AU_05 = 205,
        //POWER USER
        [Category(RoleHelpers.USER), Description(@"Shall create/edit stores")]
        PU_01 = 301,
        [Category(RoleHelpers.USER), Description(@"Shall access reports (view/download) of all stores")]
        PU_02 = 302,
        [Category(RoleHelpers.USER), Description(@"Shall search/filter reports based on defined parameters")]
        PU_03 = 303,
        [Category(RoleHelpers.USER), Description(@"Shall create users")]
        PU_04 = 304,
        [Category(RoleHelpers.USER), Description(@"Shall assign user roles and permissions")]
        PU_05 = 305,
    }
}
