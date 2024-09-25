using Microsoft.AspNetCore.Mvc;
using PalamigStore.DataAccess.Repository.IRepository;
using PalamigStore.Models;

namespace PalamigStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> productsFromDb = _unitOfWork.Product.GetAll().OrderByDescending(p => p.Id).ToList();
            return View(productsFromDb);
        }
    }
}
