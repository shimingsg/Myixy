using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myixy.App.Areas.Identity.Data
{
    public class MyixyUser : IdentityUser
    {
        [PersonalData]
        public string NickName { get; set; }

        public virtual ICollection<MyixyUserClaim> Claims { get; set; }
        public virtual ICollection<MyixyUserLogin> Logins { get; set; }
        public virtual ICollection<MyixyUserToken> Tokens { get; set; }
        public virtual ICollection<MyixyUserRole> UserRoles { get; set; }
    }

    public class MyixyRole : IdentityRole
    {
        public virtual ICollection<MyixyUserRole> UserRoles { get; set; }
        public virtual ICollection<MyixyRoleClaim> RoleClaims { get; set; }
    }

    public class MyixyUserRole : IdentityUserRole<string>
    {
        public virtual MyixyUser User { get; set; }
        public virtual MyixyRole Role { get; set; }
    }

    public class MyixyUserClaim : IdentityUserClaim<string>
    {
        public virtual MyixyUser User { get; set; }
    }

    public class MyixyUserLogin : IdentityUserLogin<string>
    {
        public virtual MyixyUser User { get; set; }
    }

    public class MyixyRoleClaim : IdentityRoleClaim<string>
    {
        public virtual MyixyRole Role { get; set; }
    }

    public class MyixyUserToken : IdentityUserToken<string>
    {
        public virtual MyixyUser User { get; set; }
    }
}
