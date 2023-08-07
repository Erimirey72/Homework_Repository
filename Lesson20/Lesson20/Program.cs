using System;
using System.Collections.Generic;
using System.Data.Entity;

class Program
{
    static void Main(string[] args)
    {

        using (var context = new ShopDbContext())
        {
            // Add a customer
            var newCustomer = new Customer { Name = "John Doe", Email = "john@example.com" };
            context.Customers.Add(newCustomer);
            context.SaveChanges();

            // Query all customers
            var customers = context.Customers.ToList();
            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.Name}, Email: {customer.Email}");
            }

            // Add a product
            var newProduct = new Product { Name = "Widget", Price = 19.99m };
            context.Products.Add(newProduct);
            context.SaveChanges();

            // Create an order with an order item
            var order = new Order { OrderDate = DateTime.Now, CustomerId = newCustomer.Id };
            order.OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = newProduct.Id, Quantity = 2 }
            };
            context.Orders.Add(order);
            context.SaveChanges();
        }
    }
}