using SQLCRUD.Models;

namespace SQLCRUD.Interfaces
{
    public interface IAbstractProductService
    {
        // Electronics Operations
        Task<Product> CreateMobilePhoneAsync(string name, string description, int stockQuantity);
        Task<Product> CreateLaptopAsync(string name, string description, int stockQuantity);
        Task<Product> CreateHeadphonesAsync(string name, string description, int stockQuantity);

        // Furniture Operations
        Task<Product> CreateSofaAsync(string name, string description, int stockQuantity);
        Task<Product> CreateTableAsync(string name, string description, int stockQuantity);
        Task<Product> CreateBedAsync(string name, string description, int stockQuantity);
        Task<Product> CreateCurtainsAsync(string name, string description, int stockQuantity);

        // Automobile Operations
        Task<Product> CreateCarAsync(string name, string description, int stockQuantity);
        Task<Product> CreateTruckAsync(string name, string description, int stockQuantity);
        Task<Product> CreateBikeAsync(string name, string description, int stockQuantity);

        // Get products by category
        Task<IEnumerable<Product>> GetElectronicsProductsAsync();
        Task<IEnumerable<Product>> GetFurnitureProductsAsync();
        Task<IEnumerable<Product>> GetAutomobileProductsAsync();

        // Get products by specific type
        Task<IEnumerable<Product>> GetMobilePhonesAsync();
        Task<IEnumerable<Product>> GetLaptopsAsync();
        Task<IEnumerable<Product>> GetHeadphonesAsync();
        Task<IEnumerable<Product>> GetSofasAsync();
        Task<IEnumerable<Product>> GetTablesAsync();
        Task<IEnumerable<Product>> GetBedsAsync();
        Task<IEnumerable<Product>> GetCurtainsAsync();
        Task<IEnumerable<Product>> GetCarsAsync();
        Task<IEnumerable<Product>> GetTrucksAsync();
        Task<IEnumerable<Product>> GetBikesAsync();
    }
}
