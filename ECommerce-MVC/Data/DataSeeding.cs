using ECommerce_MVC.Models;

namespace ECommerce_MVC.Data
{
    public class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var Scope = app.ApplicationServices.CreateScope())
            {
                var Context = Scope.ServiceProvider.GetRequiredService<DataContext>();

                Context.Database.EnsureCreated();

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
                        new Product{Name="P1",Description = "D1" ,Price=150,ImageUrl="https..." , Color=ProductColor.Red,CategoryId=1},
                        new Product{Name="P2",Description = "D2" ,Price=200,ImageUrl="https..." , Color=ProductColor.black,CategoryId=2},
                        new Product{Name="P3",Description = "D3" ,Price=250,ImageUrl="https..." , Color=ProductColor.yellow,CategoryId=3},

                    };
                    Context.AddRange(Products);
                    Context.SaveChanges();
                }

            }
        }
    }
}
