using Microsoft.EntityFrameworkCore;
using Models;

namespace BusinessLogic
{
    public class ShopDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }
    }
}