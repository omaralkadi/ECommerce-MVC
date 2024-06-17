using ECommerce_MVC.Data.Enums;
using ECommerce_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MVC.Data
{
    public class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var Scope = app.ApplicationServices.CreateScope())
            {
                var Context = Scope.ServiceProvider.GetRequiredService<DataContext>();
                Context.Database.Migrate();

                if (!Context.Categories.Any())
                {
                    var Categories = new List<Category>
                    {
                        new Category{Name="C1",Description="C1"},
                        new Category{Name="C2",Description="C2"},
                        new Category{Name="C3",Description="C3"},
                    };

                    Context.AddRange(Categories);
                    Context.SaveChanges();
                }

                if (!Context.Products.Any())
                {
                    var Products = new List<Product>
                    {
                        new Product{Name="P1",Description = "D1" ,Price=150,ImageUrl="Congee.png" , Color=ProductColor.Red,CategoryId=1},
                        new Product{Name="P2",Description = "D2" ,Price=200,ImageUrl="ChickenMarsala.png" , Color=ProductColor.black,CategoryId=2},
                        new Product{Name="P3",Description = "D3" ,Price=250,ImageUrl="PepperPasta.png" , Color=ProductColor.blue,CategoryId=3},
                        new Product{Name="P3",Description = "D3" ,Price=300,ImageUrl="VegetarianRefriedBeans.png" , Color=ProductColor.blue,CategoryId=3},

                    };
                    Context.AddRange(Products);
                    Context.SaveChanges();
                }

            }
        }
    }
}
