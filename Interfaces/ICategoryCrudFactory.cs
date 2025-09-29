using SQLCRUD.Models;

namespace SQLCRUD.Interfaces
{
    public interface ICategoryCrudFactory
    {
        // Category-based CRUD operations
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
        Task<Product> GetProductByIdAndCategoryAsync(int id, string category);
        Task<Product> CreateProductByCategoryAsync(string name, string description, string category, int stockQuantity);
        Task<Product> UpdateProductByCategoryAsync(int id, string name, string description, string category, int stockQuantity, bool isActive);
        Task<bool> DeleteProductByCategoryAsync(int id, string category);
        
        // Category-specific business logic
        Task<IEnumerable<string>> GetAvailableCategoriesAsync();
        Task<Dictionary<string, int>> GetCategoryProductCountsAsync();
        Task<bool> ValidateCategoryAsync(string category);
        
        // Category-specific product creation with specialized logic
        Product CreateElectronicsProduct(string name, string description, int stockQuantity);
        Product CreateFurnitureProduct(string name, string description, int stockQuantity);
        Product CreateAutomobileProduct(string name, string description, int stockQuantity);
        Product CreateOthersProduct(string name, string description, int stockQuantity);
    }
}
