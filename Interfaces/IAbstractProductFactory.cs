using SQLCRUD.Models;

namespace SQLCRUD.Interfaces
{
    // Abstract Factory Interface
    public interface IAbstractProductFactory
    {
        IElectronicsFactory CreateElectronicsFactory();
        IFurnitureFactory CreateFurnitureFactory();
        IAutomobileFactory CreateAutomobileFactory();
    }

    // Electronics Factory Interface
    public interface IElectronicsFactory
    {
        Product CreateMobilePhone(string name, string description, int stockQuantity);
        Product CreateLaptop(string name, string description, int stockQuantity);
        Product CreateHeadphones(string name, string description, int stockQuantity);
    }

    // Furniture Factory Interface
    public interface IFurnitureFactory
    {
        Product CreateSofa(string name, string description, int stockQuantity);
        Product CreateTable(string name, string description, int stockQuantity);
        Product CreateBed(string name, string description, int stockQuantity);
        Product CreateCurtains(string name, string description, int stockQuantity);
    }

    // Automobile Factory Interface
    public interface IAutomobileFactory
    {
        Product CreateCar(string name, string description, int stockQuantity);
        Product CreateTruck(string name, string description, int stockQuantity);
        Product CreateBike(string name, string description, int stockQuantity);
    }
}
