using Microsoft.AspNetCore.Mvc;
using PalamigStore.DataAccess.Data;
using PalamigStore.DataAccess.Repository.IRepository;
using PalamigStore.Models;

namespace PalamigStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _context;

        public CategoryController(ICategoryRepository context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categoryFromDb = _context.GetAll().OrderByDescending(c => c.Id).ToList();
            return View(categoryFromDb);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                _context.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _context.Get(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb); 
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _context.Get(c => c.Id == category.Id);

                if (existingCategory == null)
                {
                    return NotFound();
                }

                if (CategoryDetailsAreTheSame(existingCategory, category))
                {
                    ModelState.AddModelError(string.Empty, " No updates were made.");
                    return View(category);
                }

                existingCategory.Name = category.Name;
                existingCategory.Quantity = category.Quantity;

                _context.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _context.Get(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")] 
        public IActionResult DeletePost(int? id)
        {
            Category? CategoryIdFromDb = _context.Get(c => c.Id == id); 
            if (CategoryIdFromDb == null)
            {
                return NotFound();
            }

            _context.Remove(CategoryIdFromDb);
            _context.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryDetailsAreTheSame(Category existingCategory, Category category)
        {
            return existingCategory.Name == category.Name && existingCategory.Quantity == category.Quantity;
        }
    }
}
