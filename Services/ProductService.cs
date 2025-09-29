using Microsoft.EntityFrameworkCore;
using SQLCRUD.Data;
using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductFactory _productFactory;

        public ProductService(ApplicationDbContext context, IProductFactory productFactory)
        {
            _context = context;
            _productFactory = productFactory;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            var product = _productFactory.CreateProductFromDto(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id, ProductDto productDto)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return null;

            _productFactory.UpdateProduct(existingProduct, productDto);
            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return await GetAllProductsAsync();

            return await _context.Products
                .Where(p => p.Name.Contains(searchString) ||
                           p.Description.Contains(searchString) ||
                           p.Category.Contains(searchString))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            if (string.IsNullOrEmpty(category))
                return await GetAllProductsAsync();

            return await _context.Products
                .Where(p => p.Category == category)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            return await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }
    }
}
