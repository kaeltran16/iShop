using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Commons;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent.Repositories.Contracts;

namespace iShop.Web.Server.Persistent.Repositories.Commons
{
    public class ShippingRepository: DataRepositoryBase<Shipping>, IShippingRepository
    {
        public ShippingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Shipping> GetShipping(Guid id)
        {
            return await GetSingleAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Shipping>> GetShippings()
        {
            return await GetAllAsync();
        }
    }
}
