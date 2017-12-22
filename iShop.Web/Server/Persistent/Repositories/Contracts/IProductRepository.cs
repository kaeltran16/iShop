using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models;

namespace iShop.Web.Server.Persistent.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<Product> GetProductId(int id, bool includeRelated = true);
        Task<IEnumerable<Product>> GetProduct();
        void Add(Product product);
        void Remove(Product product);
    }
}
