using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Myixy.App.Areas.Identity.Data;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myixy.App.Data
{
    public static class AppDbContextSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<MyixyUser>>();
                var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                var email = "geshiming@outlook.com";
                var password = "1qaz@WSX";

                var user = new MyixyUser { UserName = email, Email = email };
                var result = await userManager.CreateAsync(user, password);
                if(result.Succeeded)
                {
                    logger.Log(NLog.LogLevel.Info, "Add seed succeeded");
                }
                else
                {
                    logger.Log(NLog.LogLevel.Warn, "Add seed succeeded");
                }

                //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // User Info
                //string userName = "myixy";
                //string email = "myixy666@gmail.com";
                //string password = "mySecur1ty!";
                //string role = "SuperAdmin";

                //if (await roleManager.FindByNameAsync(role) == null)
                //{
                //    await roleManager.CreateAsync(new IdentityRole(role));
                //}

                //if (await userManager.FindByNameAsync(userName) == null)
                //{
                //    if (await roleManager.FindByNameAsync(role) == null)
                //    {
                //        await roleManager.CreateAsync(new IdentityRole(role));
                //    }

                //    IdentityUser user = new IdentityUser
                //    {
                //        UserName = userName,
                //        Email = email
                //    };

                //    IdentityResult result = await userManager.CreateAsync(user, password);
                //    if (result.Succeeded)
                //    {
                //        await userManager.AddToRoleAsync(user, role);
                //    }
                //}
            }
        }
    }
}
