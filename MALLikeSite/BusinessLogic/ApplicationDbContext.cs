using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace BusinessLogic
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Title>()
                .Property(t => t.AverageRating)
                .HasColumnType("decimal(1, 1)");
        }
    }
}

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedAt { get; set; }

    public DateTime LastVisitedAt { get; set; }
}