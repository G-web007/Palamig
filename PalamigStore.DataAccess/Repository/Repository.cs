﻿using Microsoft.EntityFrameworkCore;
using PalamigStore.DataAccess.Data;
using PalamigStore.DataAccess.Repository.IRepository;
using System.Linq.Expressions;

namespace PalamigStore.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _context = db;
            dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList(); 
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter); 
            return query.FirstOrDefault();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);

         }

        public void Remove(T entity)
        {
            dbSet.Remove(entity); 
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
