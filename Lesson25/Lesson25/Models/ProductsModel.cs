using Models;
namespace Lesson25.Models
{
    public class ProductsModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<Product> ProductsList { get; set; } = new List<Product>();
    }
}
