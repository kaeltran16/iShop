using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Web.Server.Commons.BaseClasses;
using iShop.Web.Server.Core.Contracts;
using iShop.Web.Server.Persistent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Web.Server.Core.Commons
{
    public class DataRepositoryBase<T> : IDataRepository<T>
        where T : EntityBase, new()
    {
        protected ApplicationDbContext _context;

        protected DataRepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includeProperties != null)
                query = includeProperties(query);

            if (predicate != null)
                return await query.Where(predicate).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includeProperties != null)
                query = includeProperties(query);

            if (predicate != null)
                return await query.SingleOrDefaultAsync(predicate);

            return await query.SingleOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
        }
    }
}

