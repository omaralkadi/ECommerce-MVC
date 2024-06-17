using ECommerce_MVC.Data;
using ECommerce_MVC.Data.Interfaces;
using ECommerce_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoriesController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<IActionResult> Index()
        {
            var Categories = await _categoryRepo.getAllAsync();
            return View(Categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var Result = await _categoryRepo.CreateAsync(category);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(category);
        }

        public async Task<IActionResult> update(int id, string ViewName = "Update")
        {
            var Category = await _categoryRepo.getByIdAsync(id);
            if(Category is not null)
                 return View(ViewName, Category);
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {

            if (ModelState.IsValid)
            {
                var Result = await _categoryRepo.Update(category);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View("NotFound");
        }

        public async Task<IActionResult> Details(int id)
        {
            return await update(id, nameof(Details));
        }


        public async Task<IActionResult> Delete(int id)
        {
            return await update(id, nameof(Delete));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            if (ModelState.IsValid)
            {
                var Result = await _categoryRepo.Delete(category);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("NotFound");
        }


    }
}