using Models;


namespace BusinessLogic.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Guid Create(Product meeting);
        IEnumerable<Product> GetByPriceRange(decimal price);
        public Product Edit(Product product);
        public Product GetById(Guid id);
        public void DeleteById(Guid id);
    }
}
