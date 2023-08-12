using Microsoft.EntityFrameworkCore;
using Lesson25.Models;
using System.Collections.Generic;
using System;

namespace Lesson25.Context
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; init; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
    }
}