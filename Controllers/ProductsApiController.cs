using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLCRUD.Data;
using SQLCRUD.Models;
using System.ComponentModel.DataAnnotations;

namespace SQLCRUD.Controllers
{
    /// <summary>
    /// API Controller for managing products with full CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsApiController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Get all products with optional search and filtering
        /// </summary>
        /// <param name="searchString">Search term for name, description, or category</param>
        /// <param name="categoryFilter">Filter by specific category</param>
        /// <returns>List of products</returns>
        /// <response code="200">Returns the list of products</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
            [FromQuery] string? searchString = null,
            [FromQuery] string? categoryFilter = null)
        {
            var products = from p in _context.Products
                           select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString) ||
                                              p.Description.Contains(searchString) ||
                                              p.Category.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(categoryFilter))
            {
                products = products.Where(p => p.Category == categoryFilter);
            }

            return await products.OrderBy(p => p.Name).ToListAsync();
        }

        /// <summary>
        /// Get a specific product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product details</returns>
        /// <response code="200">Returns the requested product</response>
        /// <response code="404">Product not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            return product;
        }

        /// <summary>
        /// Get all available product categories
        /// </summary>
        /// <returns>List of unique categories</returns>
        /// <response code="200">Returns the list of categories</response>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            var categories = await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            return categories;
        }

        /// <summary>
        /// Get product statistics
        /// </summary>
        /// <returns>Product statistics summary</returns>
        /// <response code="200">Returns product statistics</response>
        [HttpGet("statistics")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<ActionResult<object>> GetStatistics()
        {
            var totalProducts = await _context.Products.CountAsync();
            var totalCategories = await _context.Products.Select(p => p.Category).Distinct().CountAsync();
            var totalValue = await _context.Products.SumAsync(p => p.Price * p.StockQuantity);
            var averagePrice = await _context.Products.AverageAsync(p => p.Price);

            return new
            {
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                TotalInventoryValue = totalValue,
                AveragePrice = averagePrice,
                LastUpdated = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product">Product data</param>
        /// <returns>Created product</returns>
        /// <response code="201">Product created successfully</response>
        /// <response code="400">Invalid product data</response>
        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive,
                DateCreated = DateTime.UtcNow
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="product">Updated product data</param>
        /// <returns>Updated product</returns>
        /// <response code="200">Product updated successfully</response>
        /// <response code="400">Invalid product data</response>
        /// <response code="404">Product not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.IsActive = product.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(existingProduct);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Success message</returns>
        /// <response code="200">Product deleted successfully</response>
        /// <response code="404">Product not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully." });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }

    /// <summary>
    /// Data Transfer Object for creating a new product
    /// </summary>
    public class CreateProductDto
    {
        /// <summary>
        /// Product name
        /// </summary>
        /// <example>Laptop Pro 15</example>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Product description
        /// </summary>
        /// <example>High-performance laptop with latest processor</example>
        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        /// <example>1299.99</example>
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Product category
        /// </summary>
        /// <example>Electronics</example>
        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Stock quantity
        /// </summary>
        /// <example>25</example>
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        /// <summary>
        /// Whether the product is active
        /// </summary>
        /// <example>true</example>
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// Data Transfer Object for updating an existing product
    /// </summary>
    public class UpdateProductDto
    {
        /// <summary>
        /// Product name
        /// </summary>
        /// <example>Laptop Pro 15</example>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Product description
        /// </summary>
        /// <example>High-performance laptop with latest processor</example>
        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        /// <example>1299.99</example>
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Product category
        /// </summary>
        /// <example>Electronics</example>
        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Stock quantity
        /// </summary>
        /// <example>25</example>
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        /// <summary>
        /// Whether the product is active
        /// </summary>
        /// <example>true</example>
        public bool IsActive { get; set; } = true;
    }
}
