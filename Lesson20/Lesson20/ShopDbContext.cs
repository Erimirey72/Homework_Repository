using Microsoft.EntityFrameworkCore;

public class ShopDbContext : System.Data.Entity.DbContext
{
    public ShopDbContext() : base("Server =.; Database=ShopDB;Trusted_Connection=True")
    {
    }

    public System.Data.Entity.DbSet<Customer> Customers { get; set; }
    public System.Data.Entity.DbSet<Employee> Employees { get; set; }
    public System.Data.Entity.DbSet<Product> Products { get; set; }
    public System.Data.Entity.DbSet<Order> Orders { get; set; }
    public System.Data.Entity.DbSet<OrderItem> OrderItems { get; set; }

    protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships between entities
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
