using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BeerTap.Data.Repository.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}
