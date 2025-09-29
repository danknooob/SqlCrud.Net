using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Factories
{
    public class ProductTypeFactory : IProductTypeFactory
    {
        private readonly IProductFactory _productFactory;

        public ProductTypeFactory(IProductFactory productFactory)
        {
            _productFactory = productFactory;
        }

        public Product CreateElectronicsProduct(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name, 
                "Electronics", 
                $"Electronic device: {description}", 
                stockQuantity, 
                true
            );
        }

        public Product CreateFurnitureProduct(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name, 
                "Furniture", 
                $"Furniture item: {description}", 
                stockQuantity, 
                true
            );
        }

        public Product CreateClothingProduct(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name, 
                "Clothing", 
                $"Clothing item: {description}", 
                stockQuantity, 
                true
            );
        }

        public Product CreateBookProduct(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name, 
                "Books", 
                $"Book: {description}", 
                stockQuantity, 
                true
            );
        }
    }
}
