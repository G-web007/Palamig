using PalamigStore.DataAccess.Data;
using PalamigStore.DataAccess.Repository.IRepository;
using PalamigStore.Models;

namespace PalamigStore.DataAccess.Repository
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }   

        public void Update(Product obj)
        {
            var objFromDb = _context.Products.FirstOrDefault(u => u.Id == obj.Id);

            if (objFromDb != null)
            {
                objFromDb.ProductName   = obj.ProductName;
                objFromDb.Description   = obj.Description;
                objFromDb.BrandName     = obj.BrandName;
                objFromDb.ListPrice     = obj.ListPrice;
                objFromDb.Price         = obj.Price;
                objFromDb.Price50       = obj.Price50;
                objFromDb.Price100      = obj.Price100;
                objFromDb.ImageUrl      = obj.ImageUrl;
                objFromDb.CategoryId    = obj.CategoryId;
            }
        }
    }
}
