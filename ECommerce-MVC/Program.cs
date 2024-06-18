using ECommerce_MVC.Data;
using ECommerce_MVC.Data.Cart;
using ECommerce_MVC.Data.Interfaces;
using ECommerce_MVC.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(x => ShoppingCart.getShoppingCart(x));
            builder.Services.AddSession();
            builder.Services.AddScoped<IOrderService, OrderService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            DataSeeding.Seed(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=Index}/{id?}");

            app.Run();
        }
    }
}