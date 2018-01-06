using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Contracts;
using iShop.Web.Server.Core.Models;

namespace iShop.Web.Server.Persistent.Repositories.Contracts
{
    public interface IProductRepository: IDataRepository<Product>
    {
        //Task<Product> GetProductId(Guid id, bool includeRelated = true);
        //Task<IEnumerable<Product>> GetProducts();
    }
}
