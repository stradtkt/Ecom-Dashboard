using Microsoft.EntityFrameworkCore;
 
namespace EcomStore.Models
{
    public class EcomContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public EcomContext(DbContextOptions<EcomContext> options) : base(options) { }

        public DbSet<Customer> customers {get;set;}
        public DbSet<Category> categories {get;set;}
        public DbSet<Product> products {get;set;}
        public DbSet<Order> orders {get;set;}
        public DbSet<ProductsCategories> products_categories  {get;set;}
    }
}