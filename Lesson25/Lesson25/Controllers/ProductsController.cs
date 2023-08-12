using Microsoft.AspNetCore.Mvc;
using Lesson25.Models;
using System.Collections.Generic;

namespace Lesson25.Controllers
{
    public class ProductsController : Controller
    {
        private static List<Product> products = new List<Product>();

        public IActionResult Index()
        {
            var model = new ProductsModel
            {
                ProductsList = products
            };
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.ID = products.Count + 1;
            products.Add(product);
            return RedirectToAction("Index");
        }
    }
}