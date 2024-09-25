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
            _context.Products.Update(obj);
        }
    }
}
