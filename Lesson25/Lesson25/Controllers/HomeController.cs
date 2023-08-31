using Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using System.Diagnostics;
using System.Globalization;
using Lesson25.Models;

namespace Lesson25.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopDbContext _products;

        public HomeController(ILogger<HomeController> logger, ShopDbContext products)
        {
            _logger = logger;
            _products = products;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();

            var products = _products.Products.ToList();

            model.Products = products;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}