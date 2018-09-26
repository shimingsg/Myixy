using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Myixy.App.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myixy.App.Data
{
    public class MyixyIdentityDbContext<TContext>
        : IdentityDbContext<
            MyixyUser,
            MyixyRole,
            string,
            MyixyUserClaim,
            MyixyUserRole,
            MyixyUserLogin,
            MyixyRoleClaim,
            MyixyUserToken>
         where TContext : DbContext
    {
        public MyixyIdentityDbContext(DbContextOptions<TContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MyixyUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.ToTable("MyixyUsers");
            });

            modelBuilder.Entity<MyixyRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();

                b.ToTable("MyixyRoles");
            });

            modelBuilder.Entity<MyixyUserClaim>(b => b.ToTable("MyixyUserClaims"));
            modelBuilder.Entity<MyixyUserClaim>(b => b.ToTable("MyixyUserRoles"));
            modelBuilder.Entity<MyixyUserClaim>(b => b.ToTable("MyixyUserLogins"));
            modelBuilder.Entity<MyixyUserClaim>(b => b.ToTable("MyixyRoleClaims"));
            modelBuilder.Entity<MyixyUserClaim>(b => b.ToTable("MyixyUserTokens"));
        }
    }
}
