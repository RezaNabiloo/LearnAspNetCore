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
        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }


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
                new Item
                {
                    Id = 1,
                    Price = 485.0M,
                    QuantityInStock = 3
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
                    ItemId = 1,
                    Name = "Leran dotnet",
                    Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.\r\n"
                },
                new Product
                {
                    Id = 2,
                    ItemId = 2,
                    Name = "Learn Python",
                    Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.\r\n"
                },
                new Product
                {
                    Id = 3,
                    ItemId = 3,
                    Name = "Learn Photoshop",
                    Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.\r\n"
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
