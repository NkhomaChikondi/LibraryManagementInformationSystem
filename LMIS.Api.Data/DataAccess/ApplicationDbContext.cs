using LMIS.Api.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Data.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }

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
        }
    }
}
