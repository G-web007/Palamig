using PalamigStore.Models;

namespace PalamigStore.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}
