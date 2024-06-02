using ECommerce_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Categories=await _context.Categories.ToListAsync();
            return View(Categories);
        }
    }
}
