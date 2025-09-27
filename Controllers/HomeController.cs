using Microsoft.AspNetCore.Mvc;
using SQLCRUD.Data;

namespace SQLCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var recentProducts = _context.Products
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.DateCreated)
                .Take(6)
                .ToList();

            var totalProducts = _context.Products.Count();
            var totalCategories = _context.Products.Select(p => p.Category).Distinct().Count();
            var totalValue = _context.Products.Sum(p => p.Price * p.StockQuantity);

            ViewBag.RecentProducts = recentProducts;
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalCategories = totalCategories;
            ViewBag.TotalValue = totalValue;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
