using Microsoft.AspNetCore.Mvc;
using Models;
using BusinessLogic;
using Lesson25.Models;
using BusinessLogic.Interfaces;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Lesson25.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IValidator<ProductsModel> _validator;

        public ProductController(ILogger<ProductController> logger, ShopDbContext shop, IProductService productService, IValidator<ProductsModel> validator)
        {
            _logger = logger;
            _productService = productService;
            _validator = validator;
        }

        private static List<Product> products = new List<Product>();

        public IActionResult Index()
        {
            var model = new IndexViewModel();

            var products = _productService.GetAll();

            model.Products = products;

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] ProductsModel model)
        {
            var validationResult = _validator.Validate(model);
            if (validationResult.IsValid)
            {
            try
            { 
                _productService.Create(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Price = model.Price
                });
            }
                catch (Exception e)
                {
                _logger.LogError(e, "Request failed. Request details: {@model}", model);
            }
        }
            else
            {
                throw new ArgumentException("Invalid input");
                return View();
            }

            return Redirect("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var product = _productService.GetById(id);

            var productModel = new EditProductModel
            {
                Id = id,
                Price = product.Price,
                Name = product.Name
            };

            return View(productModel);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(Guid id, [FromForm] EditProductModel model)
        {
            _productService.Edit(new Product
            {
                Id = model.Id,
                Price = model.Price,
                Name = model.Name
            });

        return RedirectToAction("Index", "Home");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var meeting = _productService.GetById(id);

            return View();
        }

        [HttpPost("Delete/{id}")]
        public IActionResult ConfirmDeletion(Guid id)
        {
            try
            {
                _productService.DeleteById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Request failed. Request details: {id}", id);
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpGet("{id}")]
        public IActionResult Details(Guid id)
        {
            var meeting = _productService.GetById(id);

            var meetingModel = new ProductViewModel
            {
                Id = id,
                Price = meeting.Price,
                Name = meeting.Name
            };

            return View(meetingModel);
        }
    }
}