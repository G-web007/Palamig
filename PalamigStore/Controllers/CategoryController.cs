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
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
