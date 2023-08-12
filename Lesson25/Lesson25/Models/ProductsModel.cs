namespace Lesson25.Models
{
    public class ProductsModel
    {
        public string Name { get; set; } = "Products list";

        public List<Product> ProductsList { get; set; } = new List<Product>();
    }
}
