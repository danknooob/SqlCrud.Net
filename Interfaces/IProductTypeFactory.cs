using SQLCRUD.Models;

namespace SQLCRUD.Interfaces
{
    public interface IProductTypeFactory
    {
        Product CreateElectronicsProduct(string name, string description, int stockQuantity);
        Product CreateFurnitureProduct(string name, string description, int stockQuantity);
        Product CreateClothingProduct(string name, string description, int stockQuantity);
        Product CreateBookProduct(string name, string description, int stockQuantity);
    }
}
