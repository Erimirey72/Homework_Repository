using Microsoft.EntityFrameworkCore;
using Models;

namespace BusinessLogic
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; init; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }
    }
}