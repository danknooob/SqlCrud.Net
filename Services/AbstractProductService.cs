using Microsoft.EntityFrameworkCore;
using SQLCRUD.Data;
using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Services
{
    public class AbstractProductService : IAbstractProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAbstractProductFactory _abstractFactory;

        public AbstractProductService(ApplicationDbContext context, IAbstractProductFactory abstractFactory)
        {
            _context = context;
            _abstractFactory = abstractFactory;
        }

        // Electronics Operations
        public async Task<Product> CreateMobilePhoneAsync(string name, string description, int stockQuantity)
        {
            var electronicsFactory = _abstractFactory.CreateElectronicsFactory();
            var product = electronicsFactory.CreateMobilePhone(name, description, stockQuantity);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> CreateLaptopAsync(string name, string description, int stockQuantity)
        {
            var electronicsFactory = _abstractFactory.CreateElectronicsFactory();
            var product = electronicsFactory.CreateLaptop(name, description, stockQuantity);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> CreateHeadphonesAsync(string name, string description, int stockQuantity)
        {
            var electronicsFactory = _abstractFactory.CreateElectronicsFactory();
            var product = electronicsFactory.CreateHeadphones(name, description, stockQuantity);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // Furniture Operations
        public async Task<Product> CreateSofaAsync(string name, string description, int stockQuantity)
        {
            var furnitureFactory = _abstractFactory.CreateFurnitureFactory();
            var product = furnitureFactory.CreateSofa(name, description, stockQuantity);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> CreateTableAsync(string name, string description, int stockQuantity)
        {
            var furnitureFactory = _abstractFactory.CreateFurnitureFactory();
            var product = furnitureFactory.CreateTable(name, description, stockQuantity);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> CreateBedAsync(string name, string description, int stockQuantity)
        {
            var furnitureFactory = _abstractFactory.CreateFurnitureFactory();
            var product = furnitureFactory.CreateBed(name, description, stockQuantity);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> CreateCurtainsAsync(string name, string description, int stockQuantity)
        {
            var furnitureFactory = _abstractFactory.CreateFurnitureFactory();
            var product = furnitureFactory.CreateCurtains(name, description, stockQuantity);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // Get products by specific type
        public async Task<IEnumerable<Product>> GetElectronicsProductsAsync()
        {
            return await _context.Products
                .Where(p => p.Category == "Electronics")
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetFurnitureProductsAsync()
        {
            return await _context.Products
                .Where(p => p.Category == "Furniture")
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetMobilePhonesAsync()
        {
            return await _context.Products
                .Where(p => p.Category == "Electronics" && p.Description.Contains("📱 Mobile Phone:"))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetLaptopsAsync()
        {
            return await _context.Products
                .Where(p => p.Category == "Electronics" && p.Description.Contains("💻 Laptop:"))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetHeadphonesAsync()
        {
            return await _context.Products
                .Where(p => p.Category == "Electronics" && p.Description.Contains("🎧 Headphones:"))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetSofasAsync()
        {
            return await _context.Products
                .Where(p => p.Category == "Furniture" && p.Description.Contains("🛋️ Sofa:"))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetTablesAsync()
        {
            return await _context.Products
                .Where(p => p.Category == "Furniture" && p.Description.Contains("🪑 Table:"))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetBedsAsync()
        {
            return await _context.Products
                .Where(p => p.Category == "Furniture" && p.Description.Contains("🛏️ Bed:"))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetCurtainsAsync()
        {
            return await _context.Products
                .Where(p => p.Category == "Furniture" && p.Description.Contains("🪟 Curtains:"))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}
