﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Twitter.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class TwitterUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<TwitterUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public virtual ICollection<TwitterUser> Following { get; set; }
        public virtual ICollection<TwitterUser> Followers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }

    public class TwitterDBContext : IdentityDbContext<TwitterUser>
    {
        public TwitterDBContext() 
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static TwitterDBContext Create()
        {
            return new TwitterDBContext();
        }
        public virtual DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TwitterUser>()
                .HasMany(m => m.Followers)
                .WithMany(p => p.Following)
                .Map(w => w.ToTable("User_Follow")
                .MapLeftKey("UserId")
                .MapRightKey("FollowerID"));
            base.OnModelCreating(modelBuilder);
        }
    }
}