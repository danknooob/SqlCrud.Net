using Microsoft.AspNetCore.Mvc;
using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryCrudFactory _categoryCrudFactory;

        public ProductsController(IProductService productService, ICategoryCrudFactory categoryCrudFactory)
        {
            _productService = productService;
            _categoryCrudFactory = categoryCrudFactory;
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
    }
}