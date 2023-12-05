using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T> GetByIdAsync(int id);
       // Task<T> GetByEmailAsync(string email);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null);
       // Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> filter = null);
        IQueryable<T> GetAllAsync();
        IQueryable<T> Get(Expression<Func<T, bool>> filter);
        
        Task DeleteAsync(object id);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);

    }
}
