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
            var totalProducts = _context.Products.Count();
            var totalCategories = _context.Products.Select(p => p.Category).Distinct().Count();
            var activeProducts = _context.Products.Count(p => p.IsActive);

            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalCategories = totalCategories;
            ViewBag.ActiveProducts = activeProducts;

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
