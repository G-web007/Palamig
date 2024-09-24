using Microsoft.AspNetCore.Mvc;
using PalamigStore.DataAccess.Data;
using PalamigStore.Models;

namespace PalamigStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categoryFromDb = _context.Categories.ToList();
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
                _context.Categories.Add(category);
                _context.SaveChanges();
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

            Category? categoryFromDb = _context.Categories.Find(id);

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
                var existingCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);

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

                _context.SaveChanges();
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

            Category? categoryFromDb = _context.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")] 
        public IActionResult DeletePost(int? id)
        {
            Category? CategoryIdFromDb = _context.Categories.Find(id); 
            if (CategoryIdFromDb == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(CategoryIdFromDb);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryDetailsAreTheSame(Category existingCategory, Category category)
        {
            return existingCategory.Name == category.Name && existingCategory.Quantity == category.Quantity;
        }
    }
}
