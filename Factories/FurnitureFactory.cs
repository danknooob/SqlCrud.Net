using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Factories
{
    public class FurnitureFactory : IFurnitureFactory
    {
        private readonly IProductFactory _productFactory;

        public FurnitureFactory(IProductFactory productFactory)
        {
            _productFactory = productFactory;
        }

        public Product CreateSofa(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Furniture",
                $"🛋️ Sofa: {description}",
                stockQuantity,
                true
            );
        }

        public Product CreateTable(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Furniture",
                $"🪑 Table: {description}",
                stockQuantity,
                true
            );
        }

        public Product CreateBed(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Furniture",
                $"🛏️ Bed: {description}",
                stockQuantity,
                true
            );
        }

        public Product CreateCurtains(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Furniture",
                $"🪟 Curtains: {description}",
                stockQuantity,
                true
            );
        }
    }
}
