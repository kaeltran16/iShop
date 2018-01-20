using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Contracts;
using iShop.Web.Server.Core.Models;

namespace iShop.Web.Server.Persistent.Repositories.Contracts
{
    public interface IShippingRepository: IDataRepository<Shipping>
    {
        Task<Shipping> GetShipping(Guid id);
        Task<IEnumerable<Shipping>> GetShippings();
    }
}
