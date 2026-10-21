using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityEx.Models
{
    public class CompanyContext:IdentityDbContext<User>
    {

        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
                 new Category() {CategoryID=1,CategoryName="Computer"},
                 new Category() { CategoryID = 2, CategoryName = "Home Appliances" }

                );
            builder.Entity<Product>().HasData(
                 new Product()
                 { 
                  ProductID=121,
                  ProductName="Mouse",
                  MfgName="Intex",
                  Price=450,
                  CategoryID=1

                 },
                 new Product()
                 {
                     ProductID = 122,
                     ProductName = "Keyboard",
                     MfgName = "Logitech",
                     Price = 1450,
                     CategoryID=1
                 },
                 new Product()
                 {
                     ProductID = 123,
                     ProductName = "Washing Machine",
                     MfgName = "LG",
                     Price = 2200,
                     CategoryID=2
                 }
                );
        }

        public DbSet<Product> Products { get; set; }
    }
}
