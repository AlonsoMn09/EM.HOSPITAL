using EM.Hospital.Infraestructure.Configuration.Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Infraestructure.Configuration.Authentication.Context
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<UserIdentity>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("auth");

            builder.Entity<UserIdentity>(p =>
            {
                p.ToTable("User");
            });

            builder.Entity<IdentityRole>(p =>
            {
                p.ToTable("Role");
            });

            builder.Entity<IdentityUserRole<string>>(p =>
            {
                p.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(p =>
            {
                p.ToTable("UserClaims");
            });

            builder.Entity<IdentityRoleClaim<string>>(p =>
            {
                p.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(p =>
            {
                p.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserToken<string>>(p =>
            {
                p.ToTable("UserTokens");
            });
        }
    }

    //public class AuthDbContext : IdentityDbContext<UserIdentity> 
    //{
    //    public AuthDbContext()
    //    {
            
    //    }
    //    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    //    {
            
    //    }
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseNpgsql("Host=localhost;Port=1600;Database=dbhospital;Username=admin;Password=Password2026");
    //    }

    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        base.OnModelCreating(builder);

    //        builder.HasDefaultSchema("auth");

    //        builder.Entity<UserIdentity>(p =>
    //        {
    //            p.ToTable("User");
    //        });

    //        builder.Entity<IdentityRole>(p =>
    //        {
    //            p.ToTable("Role");
    //        });

    //        builder.Entity<IdentityUserRole<string>>(p =>
    //        {
    //            p.ToTable("UserRoles");
    //        });

    //        builder.Entity<IdentityUserClaim<string>>(p =>
    //        {
    //            p.ToTable("UserClaims");
    //        });

    //        builder.Entity<IdentityRoleClaim<string>>(p =>
    //        {
    //            p.ToTable("RoleClaims");
    //        });

    //        builder.Entity<IdentityUserLogin<string>>(p =>
    //        {
    //            p.ToTable("UserLogins");
    //        });

    //        builder.Entity<IdentityUserToken<string>>(p =>
    //        {
    //            p.ToTable("UserTokens");
    //        });
    //    }
    //}
}
