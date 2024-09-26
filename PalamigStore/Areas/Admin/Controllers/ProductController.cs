using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PalamigStore.DataAccess.Repository.IRepository;
using PalamigStore.Models;
using PalamigStore.Models.ViewModels;

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
            ProductVM productVM = new ProductVM
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem 
                { 
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),

                Product = new Product()
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(productVM);
            }
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
