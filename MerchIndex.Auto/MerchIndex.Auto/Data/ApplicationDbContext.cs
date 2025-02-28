using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MerchIndex.Auto.Client.Models;

namespace MerchIndex.Auto.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();

        public DbSet<Person> People => Set<Person>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();

        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Watersports" },
                new Category { Id = 2, Name = "Soccer" },
                new Category { Id = 3, Name = "Chess" }
                );
        }
    }
}
