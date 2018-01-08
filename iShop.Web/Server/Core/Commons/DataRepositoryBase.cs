using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Contracts;
using iShop.Web.Server.Persistent;

namespace iShop.Web.Server.Core.Commons
{
    public class DataRepositoryBase<T> : IDataRepository<T>
        where T : class, new()
    {
        private readonly ApplicationDbContext _context;

        protected DataRepositoryBase(ApplicationDbContext context)
        {
            _context = context;
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

