using BusinessLogic.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Models;
using Validators;

namespace BusinessLogic.Service
{
    public class ProductService : IProductService
    {
        private readonly ShopDbContext _shop;
        private readonly IValidator<Product> _productValidator;

        public ProductService(ShopDbContext shop, IValidator<Product> productValidator)
        {
            _shop = shop;
            _productValidator = productValidator;
        }

        public IEnumerable<Product> GetAll()
        {
            return _shop.Products.ToList();
        }

        public Guid Create(Product product)
        {
            _productValidator.ValidateAndThrow(product);

            _shop.Products.Add(product);

            var result = _shop.SaveChanges();

            return product.Id;
        }

        public Product Edit(Product product)
        {
            _productValidator.ValidateAndThrow(product);

            var existingProduct = _shop.Products.FirstOrDefault(x => x.Id == product.Id);
            if (existingProduct == null)
            {
                throw new ArgumentException("No such id exists");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            _shop.SaveChanges();

            return existingProduct;
        }

        public IEnumerable<Product> GetByPriceRange(decimal price)
        {
            var baseQuery = _shop.Products.AsNoTracking();

            baseQuery = baseQuery.Where(x => x.Price >= price);

            return baseQuery.ToList();
        }

        public Product GetById(Guid id)
        {
            var result = _shop.Products.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentException("No such id exists");
            }

            return result;
        }

        public void DeleteById(Guid id)
        {
            var existingItem = _shop.Products.FirstOrDefault(x => x.Id == id);
            if (existingItem == null)
            {
                throw new ArgumentException("No such id exists");
            }

            _shop.Products.Remove(existingItem);
            _shop.SaveChanges();
        }
    }
}

