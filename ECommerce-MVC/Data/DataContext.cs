using ECommerce_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MVC.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasOne(e => e.Category)
                .WithMany(e => e.Products).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>().HasMany(e => e.Products)
                .WithOne(e => e.Category).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


    }
}
