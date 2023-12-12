using LMIS.Api.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DataAccess
{   
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Member> Members { get; set; }  
        public DbSet<MemberType> MemberTypes { get; set; }  
        public DbSet<CheckoutTransaction> CheckoutTransactions { get; set; }  
        public DbSet<BookInventory> BookInventories { get; set; }  
        public DbSet<Genre> Genres { get; set; }       
     
        public DbSet<Notification> Notifications { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite primary key for UserRole
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // Configure foreign keys
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<Member>()
           .HasOne(b => b.User)    
           .WithMany(a => a.Members)   
           .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Genre>()
          .HasOne(b => b.user)
          .WithMany(a => a.Genres)
          .HasForeignKey(b => b.userId);

            modelBuilder.Entity<Notification>()
          .HasOne(b => b.User)
          .WithMany(a => a.Notifications)
          .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Notification>()
      .HasOne(b => b.Member)
      .WithMany(a => a.Notifications)
      .HasForeignKey(b => b.MemberId);          


            modelBuilder.Entity<Member>()
          .HasOne(m => m.MemberType)
          .WithOne()
          .HasForeignKey<Member>(m => m.MemberTypeId);

            modelBuilder.Entity<CheckoutTransaction>()
          .HasOne(b => b.User)
          .WithMany(a => a.CheckoutTransactions)
          .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<CheckoutTransaction>()
          .HasOne(b => b.Member)
          .WithMany(a => a.CheckoutTransactions)
          .HasForeignKey(b => b.MemberId);

            modelBuilder.Entity<CheckoutTransaction>()
         .HasOne(b => b.BookInventory)
         .WithMany(a => a.CheckoutTransactions)
         .HasForeignKey(b => b.BookInventoryId);

            // seed member type data
            modelBuilder.Entity<MemberType>().HasData(
           new MemberType { Id = 1, Name = "Student",CreatedOn= DateTime.UtcNow },
           new MemberType { Id = 2, Name = "Staff", CreatedOn = DateTime.UtcNow },
           new MemberType { Id = 3, Name = "Regular Member", CreatedOn = DateTime.UtcNow },
           new MemberType { Id = 4, Name = "Premium Member", CreatedOn = DateTime.UtcNow },
           new MemberType { Id = 5, Name = "Guest" , CreatedOn = DateTime.UtcNow },
           new MemberType { Id = 6, Name = "Senior Citezen", CreatedOn = DateTime.UtcNow },
           new MemberType { Id = 7, Name = "Corparate Member", CreatedOn = DateTime.UtcNow });

            modelBuilder.Entity<Role>().HasData(
               new Role { RoleId = 1, RoleName = "Administrator", IsDeleted = false },
               new Role { RoleId = 2, RoleName = "FrontDesk_Officer", IsDeleted = false },
               new Role { RoleId = 3, RoleName = "Management", IsDeleted = false });
        }
    }    
}
