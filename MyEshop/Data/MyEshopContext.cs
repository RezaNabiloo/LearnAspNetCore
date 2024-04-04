using Microsoft.EntityFrameworkCore;
using MyEshop.Models;
using System.Data.Odbc;

namespace MyEshop.Data
{
    public class MyEshopContext : DbContext
    {

        public MyEshopContext(DbContextOptions<MyEshopContext> option) : base(option)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryToProduct> CategoryTpProducts { get; set; }
        public DbSet<Item> Items{ get; set; }


        #region Seed Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 1,
                Name = "ASP.Net Core",
                Description = "Learn ASP.net ore 3"
            },
            new Category()
            {
                Id = 2,
                Name = "Photoshop",
                Description = "Learning Photoshop 7"

            },
            new Category()
            {
                Id = 3,
                Name = "ورزشی",
                Description = "فعالیت های ورزشی"

            }
                );


            modelBuilder.Entity<Item>().HasData(
                new Item { 
                    Id=1,
                    Price=485.0M,
                    QuantityInStock=3
                },
                new Item
                {
                    Id = 2,
                    Price = 23M,
                    QuantityInStock = 3
                },
                new Item
                {
                    Id = 3,
                    Price = 235.6M,
                    QuantityInStock = 3
                }
                );


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ItemId=1,
                    Name="wqewqefw",
                    Description=""
                },
                new Product
                {
                    Id = 2,
                    ItemId = 2,
                    Name = "wqewqefw",
                    Description = ""
                },
                new Product
                {
                    Id = 3,
                    ItemId = 3,
                    Name = "wqewqefw",
                    Description = ""
                }
                );

            modelBuilder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct
                {
                    CategoryId = 3,
                    ProductId = 1                    
                },
                new CategoryToProduct
                {
                    CategoryId = 2,
                    ProductId = 1
                }
                );


            modelBuilder.Entity<CategoryToProduct>().HasKey(c => new { c.CategoryId, c.ProductId });
            //modelBuilder.Entity<Product>(
            //    p =>
            //    {
            //        p.HasKey(w => w.Id);
            //       // p.OwnsOne(w => w.Item);
            //        p.HasOne(w => w.Item).WithOne(w => w.Product)
            //        .HasForeignKey<Item>(w => w.Id);
            //    }
            //    );


            modelBuilder.Entity<Item>(
                i =>
                {
                    i.HasKey(w => w.Id);
                    i.Property(w => w.Price).HasColumnType("Money");
                }
                );

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
