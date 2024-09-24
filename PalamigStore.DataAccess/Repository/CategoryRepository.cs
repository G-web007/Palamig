﻿using PalamigStore.DataAccess.Data;
using PalamigStore.DataAccess.Repository.IRepository;
using PalamigStore.Models;

namespace PalamigStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category obj)
        {
            _context.Update(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
