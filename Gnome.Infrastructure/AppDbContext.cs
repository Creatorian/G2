using Gnome.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Gnome.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Variant> Variants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product->Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Variant -> Product
            modelBuilder.Entity<Variant>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique indexes
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Slug)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            modelBuilder.Entity<Variant>()
                .HasIndex(v => v.Slug)
                .IsUnique();

            // Performance indexes
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CreatedDateTime);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CreatedDateTime);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CategoryId);

            modelBuilder.Entity<Variant>()
                .HasIndex(v => v.CreatedDateTime);

            modelBuilder.Entity<Variant>()
                .HasIndex(v => v.ProductId);

            modelBuilder.Entity<Variant>()
                .HasIndex(v => v.IsPrimary);

            // Data precision
            modelBuilder.Entity<Variant>()
                .Property(v => v.Price)
                .HasPrecision(18, 2);

            // Required fields
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Category>()
                .Property(c => c.Slug)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Product>()
                .Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(1000);

            modelBuilder.Entity<Variant>()
                .Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Variant>()
                .Property(v => v.Slug)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Variant>()
                .Property(v => v.Image)
                .HasMaxLength(500);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Set CreatedDateTime for new entities
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Category || e.Entity is Product || e.Entity is Variant)
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                if (entry.Entity is Category category && category.CreatedDateTime == default)
                {
                    category.CreatedDateTime = DateTime.UtcNow;
                }
                else if (entry.Entity is Product product && product.CreatedDateTime == default)
                {
                    product.CreatedDateTime = DateTime.UtcNow;
                }
                else if (entry.Entity is Variant variant && variant.CreatedDateTime == default)
                {
                    variant.CreatedDateTime = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
