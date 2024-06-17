using ECommerce_MVC.Data;
using ECommerce_MVC.Data.Interfaces;
using ECommerce_MVC.Models;
using ECommerce_MVC.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        public ICategoryRepo _category { get; }
        public IProductRepo _product { get; }

        public ProductsController(DataContext context, IProductRepo product, ICategoryRepo category)
        {
            _context = context;
            _product = product;
            _category = category;
        }
        public async Task<IActionResult> Index(string name)
        {
            IEnumerable<Product> products;
            if(name is not null)
            {
                products = await _product.getAllAsyncByName(e=>e.Name.ToLower().Contains(name.ToLower()));
                return View(products);
            }
            products = await _product.getAllAsync();
            return View(products);
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _product.getByIdAsync(id);
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Category = await _category.getAllAsync();
            return View();
        }

        [HttpPost, ActionName(nameof(Create))]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Image is not null)
                {
                    product.ImageUrl = DocumentSetting.Upload(product.Image, "Images");
                }
                var result = await _product.CreateAsync(product);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    DocumentSetting.Delete(product.Image.Name, "Images");
                }
            }
            ViewBag.Category = await _category.getAllAsync();
            return View(product);
        }

        public async Task<IActionResult> Update(int id, string viewName = "Update")
        {
            var Product = await _product.getByIdAsync(id);
            if (Product == null)
            {
                return View("NotFound");
            }
            ViewBag.Category = await _category.getAllAsync();
            return View(viewName, Product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Image is not null)
                {
                    product.ImageUrl = DocumentSetting.Upload(product.Image, "Images");
                }
                var result = await _product.Update(product);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Category = await _category.getAllAsync();
                return View(product);

            }
            ViewBag.Category = await _category.getAllAsync();
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            return await Update(id, nameof(Delete));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            if (ModelState.IsValid)
            {
                var result = await _product.Delete(product);
                if (result > 0)
                {
                    if (product.ImageUrl is not null)
                    {
                        DocumentSetting.Delete(product.ImageUrl, "Images");

                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Category = await _category.getAllAsync();
                return View(product);
            }
            ViewBag.Category = await _category.getAllAsync();
            return View(product);
        }
    }
}