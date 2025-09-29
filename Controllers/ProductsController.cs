using Microsoft.AspNetCore.Mvc;
using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryCrudFactory _categoryCrudFactory;
        private readonly IAbstractProductService _abstractProductService;

        public ProductsController(IProductService productService, ICategoryCrudFactory categoryCrudFactory, IAbstractProductService abstractProductService)
        {
            _productService = productService;
            _categoryCrudFactory = categoryCrudFactory;
            _abstractProductService = abstractProductService;
        }

        // GET: Products - Redirect to category selection
        public IActionResult Index()
        {
            return RedirectToAction(nameof(CategorySelection));
        }

        // GET: Products/CategorySelection - Choose category to work with
        public async Task<IActionResult> CategorySelection()
        {
            var categoryCounts = await _categoryCrudFactory.GetCategoryProductCountsAsync();
            var allProducts = await _productService.GetAllProductsAsync();
            
            ViewBag.CategoryCounts = categoryCounts;
            ViewBag.AllProducts = allProducts;
            
            return View();
        }

        // ===== CATEGORY-BASED CRUD OPERATIONS =====

        // GET: Products/Category/{category}
        public async Task<IActionResult> CategoryIndex(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return RedirectToAction(nameof(Index));
            }

            var products = await _categoryCrudFactory.GetProductsByCategoryAsync(category);
            var categoryCounts = await _categoryCrudFactory.GetCategoryProductCountsAsync();
            
            ViewBag.Category = category;
            ViewBag.CategoryCounts = categoryCounts;
            ViewBag.CurrentCategory = category;

            return View(products);
        }

        // GET: Products/Category/{category}/Create
        public IActionResult CategoryCreate(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Category = category;
            return View();
        }

        // POST: Products/Category/{category}/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(string category, string name, string description, int stockQuantity)
        {
            try
            {
                var product = await _categoryCrudFactory.CreateProductByCategoryAsync(name, description, category, stockQuantity);
                TempData["SuccessMessage"] = $"{category} product created successfully!";
                return RedirectToAction(nameof(CategoryIndex), new { category });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error creating {category} product: " + ex.Message;
                ViewBag.Category = category;
                return View();
            }
        }

        // GET: Products/Category/{category}/Details/{id}
        public async Task<IActionResult> CategoryDetails(string category, int id)
        {
            var product = await _categoryCrudFactory.GetProductByIdAndCategoryAsync(id, category);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found in this category.";
                return RedirectToAction(nameof(CategoryIndex), new { category });
            }

            ViewBag.Category = category;
            return View(product);
        }

        // GET: Products/Category/{category}/Edit/{id}
        public async Task<IActionResult> CategoryEdit(string category, int id)
        {
            var product = await _categoryCrudFactory.GetProductByIdAndCategoryAsync(id, category);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found in this category.";
                return RedirectToAction(nameof(CategoryIndex), new { category });
            }

            ViewBag.Category = category;
            return View(product);
        }

        // POST: Products/Category/{category}/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(string category, int id, string name, string description, int stockQuantity, bool isActive)
        {
            try
            {
                var product = await _categoryCrudFactory.UpdateProductByCategoryAsync(id, name, description, category, stockQuantity, isActive);
                if (product == null)
                {
                    TempData["ErrorMessage"] = "Product not found in this category.";
                    return RedirectToAction(nameof(CategoryIndex), new { category });
                }

                TempData["SuccessMessage"] = $"{category} product updated successfully!";
                return RedirectToAction(nameof(CategoryIndex), new { category });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error updating {category} product: " + ex.Message;
                ViewBag.Category = category;
                return View();
            }
        }

        // GET: Products/Category/{category}/Delete/{id}
        public async Task<IActionResult> CategoryDelete(string category, int id)
        {
            var product = await _categoryCrudFactory.GetProductByIdAndCategoryAsync(id, category);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found in this category.";
                return RedirectToAction(nameof(CategoryIndex), new { category });
            }

            ViewBag.Category = category;
            return View(product);
        }

        // POST: Products/Category/{category}/Delete/{id}
        [HttpPost, ActionName("CategoryDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDeleteConfirmed(string category, int id)
        {
            try
            {
                var result = await _categoryCrudFactory.DeleteProductByCategoryAsync(id, category);
                if (result)
                {
                    TempData["SuccessMessage"] = $"{category} product deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found in this category.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting {category} product: " + ex.Message;
            }

            return RedirectToAction(nameof(CategoryIndex), new { category });
        }

        // ===== ABSTRACT FACTORY PATTERN OPERATIONS =====

        // Electronics Abstract Factory Actions
        public async Task<IActionResult> ElectronicsAbstract()
        {
            var products = await _abstractProductService.GetElectronicsProductsAsync();
            ViewBag.Category = "Electronics";
            ViewBag.FactoryType = "Abstract Factory";
            return View(products);
        }

        public IActionResult CreateMobilePhone()
        {
            ViewBag.ProductType = "Mobile Phone";
            ViewBag.Category = "Electronics";
            return View("CreateAbstractProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMobilePhone(string name, string description, int stockQuantity)
        {
            try
            {
                await _abstractProductService.CreateMobilePhoneAsync(name, description, stockQuantity);
                TempData["SuccessMessage"] = "Mobile Phone created successfully using Abstract Factory!";
                return RedirectToAction(nameof(ElectronicsAbstract));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating mobile phone: " + ex.Message;
                ViewBag.ProductType = "Mobile Phone";
                ViewBag.Category = "Electronics";
                return View();
            }
        }

        public IActionResult CreateLaptop()
        {
            ViewBag.ProductType = "Laptop";
            ViewBag.Category = "Electronics";
            return View("CreateAbstractProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLaptop(string name, string description, int stockQuantity)
        {
            try
            {
                await _abstractProductService.CreateLaptopAsync(name, description, stockQuantity);
                TempData["SuccessMessage"] = "Laptop created successfully using Abstract Factory!";
                return RedirectToAction(nameof(ElectronicsAbstract));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating laptop: " + ex.Message;
                ViewBag.ProductType = "Laptop";
                ViewBag.Category = "Electronics";
                return View();
            }
        }

        public IActionResult CreateHeadphones()
        {
            ViewBag.ProductType = "Headphones";
            ViewBag.Category = "Electronics";
            return View("CreateAbstractProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHeadphones(string name, string description, int stockQuantity)
        {
            try
            {
                await _abstractProductService.CreateHeadphonesAsync(name, description, stockQuantity);
                TempData["SuccessMessage"] = "Headphones created successfully using Abstract Factory!";
                return RedirectToAction(nameof(ElectronicsAbstract));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating headphones: " + ex.Message;
                ViewBag.ProductType = "Headphones";
                ViewBag.Category = "Electronics";
                return View();
            }
        }

        // Furniture Abstract Factory Actions
        public async Task<IActionResult> FurnitureAbstract()
        {
            var products = await _abstractProductService.GetFurnitureProductsAsync();
            ViewBag.Category = "Furniture";
            ViewBag.FactoryType = "Abstract Factory";
            return View(products);
        }

        public IActionResult CreateSofa()
        {
            ViewBag.ProductType = "Sofa";
            ViewBag.Category = "Furniture";
            return View("CreateAbstractProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSofa(string name, string description, int stockQuantity)
        {
            try
            {
                await _abstractProductService.CreateSofaAsync(name, description, stockQuantity);
                TempData["SuccessMessage"] = "Sofa created successfully using Abstract Factory!";
                return RedirectToAction(nameof(FurnitureAbstract));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating sofa: " + ex.Message;
                ViewBag.ProductType = "Sofa";
                ViewBag.Category = "Furniture";
                return View();
            }
        }

        public IActionResult CreateTable()
        {
            ViewBag.ProductType = "Table";
            ViewBag.Category = "Furniture";
            return View("CreateAbstractProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTable(string name, string description, int stockQuantity)
        {
            try
            {
                await _abstractProductService.CreateTableAsync(name, description, stockQuantity);
                TempData["SuccessMessage"] = "Table created successfully using Abstract Factory!";
                return RedirectToAction(nameof(FurnitureAbstract));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating table: " + ex.Message;
                ViewBag.ProductType = "Table";
                ViewBag.Category = "Furniture";
                return View();
            }
        }

        public IActionResult CreateBed()
        {
            ViewBag.ProductType = "Bed";
            ViewBag.Category = "Furniture";
            return View("CreateAbstractProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBed(string name, string description, int stockQuantity)
        {
            try
            {
                await _abstractProductService.CreateBedAsync(name, description, stockQuantity);
                TempData["SuccessMessage"] = "Bed created successfully using Abstract Factory!";
                return RedirectToAction(nameof(FurnitureAbstract));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating bed: " + ex.Message;
                ViewBag.ProductType = "Bed";
                ViewBag.Category = "Furniture";
                return View();
            }
        }

        public IActionResult CreateCurtains()
        {
            ViewBag.ProductType = "Curtains";
            ViewBag.Category = "Furniture";
            return View("CreateAbstractProduct");
        }

        // Automobile Abstract Factory Actions
        public async Task<IActionResult> AutomobileAbstract()
        {
            var products = await _abstractProductService.GetAutomobileProductsAsync();
            ViewBag.Category = "Automobile";
            ViewBag.FactoryType = "Abstract Factory";
            return View(products);
        }

        public IActionResult CreateCar()
        {
            ViewBag.ProductType = "Car";
            ViewBag.Category = "Automobile";
            return View("CreateAbstractProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCar(string name, string description, int stockQuantity)
        {
            try
            {
                await _abstractProductService.CreateCarAsync(name, description, stockQuantity);
                TempData["SuccessMessage"] = "Car created successfully using Abstract Factory!";
                return RedirectToAction(nameof(AutomobileAbstract));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating car: " + ex.Message;
                ViewBag.ProductType = "Car";
                ViewBag.Category = "Automobile";
                return View("CreateAbstractProduct");
            }
        }

        public IActionResult CreateTruck()
        {
            ViewBag.ProductType = "Truck";
            ViewBag.Category = "Automobile";
            return View("CreateAbstractProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTruck(string name, string description, int stockQuantity)
        {
            try
            {
                await _abstractProductService.CreateTruckAsync(name, description, stockQuantity);
                TempData["SuccessMessage"] = "Truck created successfully using Abstract Factory!";
                return RedirectToAction(nameof(AutomobileAbstract));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating truck: " + ex.Message;
                ViewBag.ProductType = "Truck";
                ViewBag.Category = "Automobile";
                return View("CreateAbstractProduct");
            }
        }

        public IActionResult CreateBike()
        {
            ViewBag.ProductType = "Bike";
            ViewBag.Category = "Automobile";
            return View("CreateAbstractProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBike(string name, string description, int stockQuantity)
        {
            try
            {
                await _abstractProductService.CreateBikeAsync(name, description, stockQuantity);
                TempData["SuccessMessage"] = "Bike created successfully using Abstract Factory!";
                return RedirectToAction(nameof(AutomobileAbstract));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating bike: " + ex.Message;
                ViewBag.ProductType = "Bike";
                ViewBag.Category = "Automobile";
                return View("CreateAbstractProduct");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCurtains(string name, string description, int stockQuantity)
        {
            try
            {
                await _abstractProductService.CreateCurtainsAsync(name, description, stockQuantity);
                TempData["SuccessMessage"] = "Curtains created successfully using Abstract Factory!";
                return RedirectToAction(nameof(FurnitureAbstract));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating curtains: " + ex.Message;
                ViewBag.ProductType = "Curtains";
                ViewBag.Category = "Furniture";
                return View();
            }
        }
    }
}