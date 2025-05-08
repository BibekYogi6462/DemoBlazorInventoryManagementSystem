using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<InventoryLog> InventoryLogs => Set<InventoryLog>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<InventoryLog>()
                .HasOne(i => i.Product)
                .WithMany(p => p.InventoryLogs)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<InventoryLog>()
                .HasOne(i => i.User)
                .WithMany(u => u.InventoryLogs)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
    }
}
