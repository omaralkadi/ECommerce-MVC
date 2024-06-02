using ECommerce_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products=await _context.Products.Include(x=>x.Category).OrderBy(x=>x.Price).ToListAsync();

            return View(products);
        }
    }
}
