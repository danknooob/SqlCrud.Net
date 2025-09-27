using Microsoft.EntityFrameworkCore;
using SQLCRUD.Models;

namespace SQLCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop Pro 15",
                    Description = "High-performance laptop with latest processor",
                    Price = 1299.99m,
                    Category = "Electronics",
                    StockQuantity = 25,
                    DateCreated = DateTime.Now.AddDays(-30),
                    IsActive = true
                },
                new Product
                {
                    Id = 2,
                    Name = "Wireless Mouse",
                    Description = "Ergonomic wireless mouse with long battery life",
                    Price = 49.99m,
                    Category = "Electronics",
                    StockQuantity = 100,
                    DateCreated = DateTime.Now.AddDays(-15),
                    IsActive = true
                },
                new Product
                {
                    Id = 3,
                    Name = "Office Chair",
                    Description = "Comfortable ergonomic office chair",
                    Price = 299.99m,
                    Category = "Furniture",
                    StockQuantity = 15,
                    DateCreated = DateTime.Now.AddDays(-7),
                    IsActive = true
                }
            );
        }
    }
}
