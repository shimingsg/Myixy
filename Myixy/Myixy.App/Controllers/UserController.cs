using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Myixy.App.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<IdentityUser> userManager;

        public UserController(UserManager<IdentityUser> userMgr)
        {
            userManager = userMgr;
        }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }
    }
}