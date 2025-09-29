using SQLCRUD.Interfaces;

namespace SQLCRUD.Factories
{
    public class AbstractProductFactory : IAbstractProductFactory
    {
        private readonly IProductFactory _productFactory;

        public AbstractProductFactory(IProductFactory productFactory)
        {
            _productFactory = productFactory;
        }

        public IElectronicsFactory CreateElectronicsFactory()
        {
            return new ElectronicsFactory(_productFactory);
        }

        public IFurnitureFactory CreateFurnitureFactory()
        {
            return new FurnitureFactory(_productFactory);
        }

        public IAutomobileFactory CreateAutomobileFactory()
        {
            return new AutomobileFactory(_productFactory);
        }
    }
}
