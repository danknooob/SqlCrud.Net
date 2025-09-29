using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Factories
{
    public class ElectronicsFactory : IElectronicsFactory
    {
        private readonly IProductFactory _productFactory;

        public ElectronicsFactory(IProductFactory productFactory)
        {
            _productFactory = productFactory;
        }

        public Product CreateMobilePhone(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Electronics",
                $"📱 Mobile Phone: {description}",
                stockQuantity,
                true
            );
        }

        public Product CreateLaptop(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Electronics",
                $"💻 Laptop: {description}",
                stockQuantity,
                true
            );
        }

        public Product CreateHeadphones(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Electronics",
                $"🎧 Headphones: {description}",
                stockQuantity,
                true
            );
        }
    }
}
