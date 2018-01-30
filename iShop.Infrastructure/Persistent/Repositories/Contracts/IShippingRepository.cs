using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Core.Entities;
using iShop.Infrastructure.Data;

namespace iShop.Infrastructure.Persistent.Repositories.Contracts
{
    public interface IShippingRepository : IDataRepository<Shipping>
    {
        Task<Shipping> GetShipping(Guid id, bool isIncludeRelative = true);
        Task<IEnumerable<Shipping>> GetShippings(bool isIncludeRelative = true);
    }
}
