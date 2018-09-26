using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Myixy.App.Areas.Identity.Data;
using Myixy.App.Models;

namespace Myixy.App.Data
{
    public class AppDbContext : MyixyIdentityDbContext<AppDbContext>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Heartfelt> Heartfelts { get; set; }
    }
}
