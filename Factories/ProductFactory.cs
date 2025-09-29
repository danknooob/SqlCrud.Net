using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Factories
{
    public class ProductFactory : IProductFactory
    {
        public Product CreateProduct(string name, string category, string description, int stockQuantity, bool isActive = true)
        {
            return new Product
            {
                Name = name,
                Category = category,
                Description = description,
                StockQuantity = stockQuantity,
                IsActive = isActive,
                DateCreated = DateTime.Now
            };
        }

        public Product CreateProductFromDto(ProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Category = dto.Category,
                Description = dto.Description,
                StockQuantity = dto.StockQuantity,
                IsActive = dto.IsActive,
                DateCreated = dto.DateCreated
            };
        }

        public Product UpdateProduct(Product existingProduct, ProductDto dto)
        {
            existingProduct.Name = dto.Name;
            existingProduct.Category = dto.Category;
            existingProduct.Description = dto.Description;
            existingProduct.StockQuantity = dto.StockQuantity;
            existingProduct.IsActive = dto.IsActive;
            // Keep original DateCreated
            return existingProduct;
        }
    }
}
