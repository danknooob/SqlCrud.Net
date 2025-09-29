using Microsoft.AspNetCore.Mvc;
using SQLCRUD.Interfaces;
using SQLCRUD.Models;

namespace SQLCRUD.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString, string categoryFilter)
        {
            IEnumerable<Product> products;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = await _productService.SearchProductsAsync(searchString);
            }
            else if (!string.IsNullOrEmpty(categoryFilter))
            {
                products = await _productService.GetProductsByCategoryAsync(categoryFilter);
            }
            else
            {
                products = await _productService.GetAllProductsAsync();
            }

            var categories = await _productService.GetCategoriesAsync();

            ViewBag.Categories = categories;
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = categoryFilter;

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View(new ProductDto());
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.CreateProductAsync(productDto);
                    TempData["SuccessMessage"] = "Product created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error creating product: " + ex.Message;
                    return View(productDto);
                }
            }
            
            return View(productDto);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive,
                DateCreated = product.DateCreated
            };

            return View(productDto);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedProduct = await _productService.UpdateProductAsync(id, productDto);
                    if (updatedProduct == null)
                    {
                        return NotFound();
                    }
                    TempData["SuccessMessage"] = "Product updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error updating product: " + ex.Message;
                    return View(productDto);
                }
            }
            return View(productDto);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Product deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting product: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
