using Gnome.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Variant>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Slug)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            modelBuilder.Entity<Variant>()
                .HasIndex(v => v.Slug)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CreatedDateTime);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CreatedDateTime);

            modelBuilder.Entity<Variant>()
                .HasIndex(v => v.CreatedDateTime);

            modelBuilder.Entity<Variant>()
                .HasIndex(v => v.ProductId);

            modelBuilder.Entity<Variant>()
                .HasIndex(v => v.IsPrimary);

            modelBuilder.Entity<Variant>()
                .Property(v => v.Price)
                .HasPrecision(18, 2);

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
                .HasMaxLength(500)
                .IsRequired(false);

            modelBuilder.Entity<AdminUser>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<AdminUser>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<AdminUser>()
                .Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<AdminUser>()
                .Property(u => u.FirstName)
                .HasMaxLength(100);

            modelBuilder.Entity<AdminUser>()
                .Property(u => u.LastName)
                .HasMaxLength(100);

            modelBuilder.Entity<AdminUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<AdminUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<RefreshToken>()
                .Property(rt => rt.ExpiresAt)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .Property(rt => rt.IsRevoked)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .Property(rt => rt.CreatedAt)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .Property(rt => rt.AdminUserId)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.Token)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.AdminUser)
                .WithMany(au => au.RefreshTokens)
                .HasForeignKey(rt => rt.AdminUserId)
                .OnDelete(DeleteBehavior.Cascade);
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

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("GnomeConnection");
            
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
