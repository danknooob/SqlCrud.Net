using Microsoft.EntityFrameworkCore;
using SQLCRUD.Data;
using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Factories
{
    public class CategoryCrudFactory : ICategoryCrudFactory
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductFactory _productFactory;

        public CategoryCrudFactory(ApplicationDbContext context, IProductFactory productFactory)
        {
            _context = context;
            _productFactory = productFactory;
        }

        // Category-based CRUD operations
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _context.Products
                .Where(p => p.Category.ToLower() == category.ToLower())
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAndCategoryAsync(int id, string category)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.Category.ToLower() == category.ToLower());
        }

        public async Task<Product> CreateProductByCategoryAsync(string name, string description, string category, int stockQuantity)
        {
            var product = CreateProductByCategory(name, description, category, stockQuantity);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductByCategoryAsync(int id, string name, string description, string category, int stockQuantity, bool isActive)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.Category.ToLower() == category.ToLower());
            
            if (product == null)
                return null;

            product.Name = name;
            product.Description = description;
            product.StockQuantity = stockQuantity;
            product.IsActive = isActive;

            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductByCategoryAsync(int id, string category)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.Category.ToLower() == category.ToLower());
            
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        // Category-specific business logic
        public async Task<IEnumerable<string>> GetAvailableCategoriesAsync()
        {
            return await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetCategoryProductCountsAsync()
        {
            return await _context.Products
                .GroupBy(p => p.Category)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        public async Task<bool> ValidateCategoryAsync(string category)
        {
            var validCategories = new[] { "Electronics", "Furniture", "Automobile", "Others" };
            return validCategories.Contains(category, StringComparer.OrdinalIgnoreCase);
        }

        // Category-specific product creation with specialized logic
        public Product CreateElectronicsProduct(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Electronics",
                $"🔌 Electronic device: {description}",
                stockQuantity,
                true
            );
        }

        public Product CreateFurnitureProduct(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Furniture",
                $"🪑 Furniture item: {description}",
                stockQuantity,
                true
            );
        }

        public Product CreateAutomobileProduct(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Automobile",
                $"🚗 Automobile: {description}",
                stockQuantity,
                true
            );
        }

        public Product CreateOthersProduct(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Others",
                $"📦 Other item: {description}",
                stockQuantity,
                true
            );
        }

        // Private helper method for category-based creation
        private Product CreateProductByCategory(string name, string description, string category, int stockQuantity)
        {
            return category.ToLower() switch
            {
                "electronics" => CreateElectronicsProduct(name, description, stockQuantity),
                "furniture" => CreateFurnitureProduct(name, description, stockQuantity),
                "automobile" => CreateAutomobileProduct(name, description, stockQuantity),
                "others" => CreateOthersProduct(name, description, stockQuantity),
                _ => _productFactory.CreateProduct(name, description, category, stockQuantity, true)
            };
        }
    }
}
