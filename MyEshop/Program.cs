using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using System.Drawing;

namespace MyEshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region DbContext
            builder.Services.AddDbContext<MyEshopContext>(options =>
            options.UseSqlServer("Data Source=.; Initial Catalog  = EshopCore_DB; Integrated Security = true; TrustServerCertificate=True"));
            
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}