using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myixy.App.Areas.Identity.Data
{
    public class MyixyUser: IdentityUser
    {
        [PersonalData]
        public string NickName { get; set; }
    }
}
