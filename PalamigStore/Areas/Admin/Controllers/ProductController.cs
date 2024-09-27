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
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Create(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\Product");
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\Product\" + fileName;
                }else
                {
                    ModelState.AddModelError(string.Empty, "Please choose an image.");
                    productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });

                    return View(productVM);
                }

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

            ProductVM productVM = new ProductVM
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),

                Product = ProductFromDb
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                 
                    string productPath = Path.Combine(wwwRootPath, @"images\Product");

                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl)) 
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('/')); 

                        if (System.IO.File.Exists(oldImagePath)) 
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImageUrl = @"\images\Product\" + fileName;
                }

                var existingProduct = _unitOfWork.Product.Get(p => p.Id == obj.Product.Id);

                if (existingProduct == null)
                {
                    return NotFound();
                }
                
                if (ProductDetailsAreTheSame(existingProduct, obj))
                {
                    ModelState.AddModelError(string.Empty, " No updates made.");

                    obj.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }).ToList();

                    return View(obj);
                }

                _unitOfWork.Product.Update(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                // If ModelState is not valid, return the view with the original productVM
                obj.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(obj);
            }
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

        private bool ProductDetailsAreTheSame(Product existingProduct, ProductVM productVM)
        {
            return existingProduct.ProductName == productVM.Product.ProductName &&
                   existingProduct.Description == productVM.Product.Description &&
                   existingProduct.BrandName   == productVM.Product.BrandName &&
                   existingProduct.ListPrice   == productVM.Product.ListPrice &&
                   existingProduct.Price       == productVM.Product.Price &&
                   existingProduct.Price50     == productVM.Product.Price50 &&
                   existingProduct.Price100    == productVM.Product.Price100 &&
                   existingProduct.ImageUrl    == productVM.Product.ImageUrl &&
                   existingProduct.CategoryId  == productVM.Product.CategoryId;
        }
    }
}
