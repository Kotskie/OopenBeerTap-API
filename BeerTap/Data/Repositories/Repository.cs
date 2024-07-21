using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BeerTap.Data.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BarDbContext Context;

        public Repository(BarDbContext context)
        {
            Context = context;
        }

        public virtual async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            //await Context.SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
            //await Context.SaveChangesAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = Context.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var orders = await Context.Set<T>().ToListAsync();
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual async Task RemoveAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            //await Context.SaveChangesAsync();
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
            //await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            //await Context.SaveChangesAsync();
        }
    }
}
