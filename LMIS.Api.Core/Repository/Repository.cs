using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _db.Set<T>() ?? throw new NullReferenceException($"DbSet<{typeof(T)}> is null");
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(filter);
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity).ConfigureAwait(false);
        }

        public async Task DeleteAsync(object id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var entityToDelete = await _dbSet.FindAsync(id).ConfigureAwait(false);
            if (entityToDelete != null)
                await DeleteAsync(entityToDelete).ConfigureAwait(false);
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (_db.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
            await Task.CompletedTask.ConfigureAwait(false); 
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter).ConfigureAwait(false);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter).AsNoTracking();
        }

        public IQueryable<T> GetAllAsync()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T? entity = await _dbSet.FindAsync(id).ConfigureAwait(false);
            if (entity == null)
            {
                throw new InvalidOperationException("Entity not found");
            }

            return entity;
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null)
        {
            return await _dbSet.FirstOrDefaultAsync(filter).ConfigureAwait(false) ?? throw new InvalidOperationException($"No entity found for type {typeof(T)}");
        }
    }
}
