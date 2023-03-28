using Microsoft.EntityFrameworkCore;
using SimplestGraphQL.Models;

namespace SimplestGraphQL.Context
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        // 使用EFCore内存数据库，并添加一些数据
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ProductDb");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Apple", Price = 1.99m },
                new Product { Id = 2, Name = "Banana", Price = 0.99m },
                new Product { Id = 3, Name = "Orange", Price = 1.49m }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
