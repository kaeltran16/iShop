using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Web.Server.Core.Contracts
{
    public interface IDataRepository { }

    public interface IDataRepository<T> : IDataRepository
        where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null);
        Task AddAsync(T entity);
        void Remove(T entity);
    }
}
