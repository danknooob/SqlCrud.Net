using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Factories
{
    public class AutomobileFactory : IAutomobileFactory
    {
        private readonly IProductFactory _productFactory;

        public AutomobileFactory(IProductFactory productFactory)
        {
            _productFactory = productFactory;
        }

        public Product CreateCar(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Automobile",
                $"🚗 Car: {description}",
                stockQuantity,
                true
            );
        }

        public Product CreateTruck(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Automobile",
                $"🚛 Truck: {description}",
                stockQuantity,
                true
            );
        }

        public Product CreateBike(string name, string description, int stockQuantity)
        {
            return _productFactory.CreateProduct(
                name,
                "Automobile",
                $"🏍️ Bike: {description}",
                stockQuantity,
                true
            );
        }
    }
}
