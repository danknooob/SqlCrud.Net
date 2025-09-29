using SQLCRUD.Models;

namespace SQLCRUD.Interfaces
{
    public interface IProductFactory
    {
        Product CreateProduct(string name, string category, string description, int stockQuantity, bool isActive = true);
        Product CreateProductFromDto(ProductDto dto);
        Product UpdateProduct(Product existingProduct, ProductDto dto);
    }
}
