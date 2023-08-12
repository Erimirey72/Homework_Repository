using Lesson25.Models;
using Microsoft.AspNetCore.Mvc;
using Lesson25.Context;
using System;
using System.Diagnostics;

namespace Lesson25.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductDbContext _products;

        public HomeController(ILogger<HomeController> logger, ProductDbContext products)
        {
            _logger = logger;
            _products = products;
        }

        public IActionResult Index()
        {
            var model = new ProductsModel();

            model.ProductsList = _products.Products.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _products.Products.Add(product);
            _products.SaveChanges();

            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
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