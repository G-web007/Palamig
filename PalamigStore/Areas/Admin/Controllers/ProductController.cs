using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
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

            Product? ProductFromDb = _unitOfWork.Product.Get(x => x.Id == id);

            if (ProductFromDb == null)
            {
                return NotFound();
            }

            return View(ProductFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _unitOfWork.Product.Get(c => c.Id == obj.Id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                if (ProductDetailsAreTheSame(existingProduct, obj))
                {
                    ModelState.AddModelError(string.Empty, " No updates were made.");
                    return View(obj);
                }

                existingProduct.ProductName = obj.ProductName;
                existingProduct.Description = obj.Description;
                existingProduct.BrandName   = obj.BrandName;
                existingProduct.ListPrice   = obj.ListPrice;
                existingProduct.Price       = obj.Price;
                existingProduct.Price50     = obj.Price50;
                existingProduct.Price100    = obj.Price100;

                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDb = _unitOfWork.Product.Get(x => x.Id == id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "Product deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool ProductDetailsAreTheSame(Product existingProduct, Product obj)
        {
            return existingProduct.ProductName == obj.ProductName && existingProduct.Description == obj.Description && existingProduct.BrandName == obj.BrandName && existingProduct.ListPrice == obj.ListPrice && existingProduct.Price == obj.Price && existingProduct.Price50 == obj.Price50 && existingProduct.Price100 == obj.Price100;
        }
    }
}
