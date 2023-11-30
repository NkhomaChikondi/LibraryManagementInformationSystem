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
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<Member> members { get; set; }  
        public DbSet<MemberType> memberTypes { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite primary key for UserRole
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.userId, ur.roleId });

            // Configure foreign keys
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.userRoles)
                .HasForeignKey(ur => ur.userId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.userRoles)
                .HasForeignKey(ur => ur.roleId);

            modelBuilder.Entity<Member>()
           .HasOne(b => b.user)    
           .WithMany(a => a.members)   
           .HasForeignKey(b => b.userId);

            modelBuilder.Entity<Member>()
          .HasOne(m => m.memberType)
          .WithOne()
          .HasForeignKey<Member>(m => m.MemberTypeId);

            modelBuilder.Entity<CheckoutTransaction>()
          .HasOne(b => b.user)
          .WithMany(a => a.checkoutTransactions)
          .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<CheckoutTransaction>()
          .HasOne(b => b.member)
          .WithMany(a => a.checkoutTransactions)
          .HasForeignKey(b => b.MemberId);

            modelBuilder.Entity<CheckoutTransaction>()
         .HasOne(b => b.Inventory)
         .WithMany(a => a.checkoutTransactions)
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
        }
    }    
}
