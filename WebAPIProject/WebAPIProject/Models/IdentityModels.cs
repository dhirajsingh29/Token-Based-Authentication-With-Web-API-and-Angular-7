﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace WebAPIProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AspNetUsers -> Users
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            // AspNetRoles -> Roles
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            //AspNetUserRoles -> UserRole
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            //AspNetUserClaims -> UserClaim
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            //AspNetUserLogins -> UserLogin
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        }
    }
}