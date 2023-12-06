using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
          _db = dbContext;   
            _dbSet = _db.Set<T>();
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(filter);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);           
            
        }

        public async Task DeleteAsync(object id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);
            if (entityToDelete != null)
                await DeleteAsync(entityToDelete);
        }

        public async Task DeleteAsync(T entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

             _dbSet.Remove(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter).AsNoTracking();
        }

        public IQueryable<T> GetAllAsync()
        {
            return _dbSet.AsNoTracking();
        }

       
        //public async Task<T> GetByEmailAsync(string email)
        //{
        //    return await _dbSet.FindAsync(ema
        //}

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }
        //public async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> filter = null)
        //{
        //    return await _dbSet.LastOrDefaultAsync(filter);
        //}


    }
}
