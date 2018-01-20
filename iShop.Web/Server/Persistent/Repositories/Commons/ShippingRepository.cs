using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Commons;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace iShop.Web.Server.Persistent.Repositories.Commons
{
    public class ShippingRepository: DataRepositoryBase<Shipping>, IShippingRepository
    {
        public ShippingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Shipping> GetShipping(Guid id, bool isIncludeRelative = true)
        {
            Expression<Func<Shipping, bool>> predicate = o => o.Id == id;
              return isIncludeRelative
                ? await GetSingleAsync(
                    predicate: predicate,
                    includeProperties: source => source
                        .Include(o => o.Order))
                : await GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<Shipping>> GetShippings(bool isIncludeRelative = true)
        {
            return isIncludeRelative
                ? await GetAllAsync(includeProperties: source => source
                    .Include(o => o.Order))
                : await GetAllAsync();
        }
    }
}
