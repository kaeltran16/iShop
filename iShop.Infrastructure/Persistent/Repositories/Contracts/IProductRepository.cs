using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Core.Entities;
using iShop.Infrastructure.Data;

namespace iShop.Infrastructure.Persistent.Repositories.Contracts
{
    public interface IProductRepository : IDataRepository<Product>
    {
        Task<Product> GetProduct(Guid id, bool isIncludeRelative = true);
        Task<IEnumerable<Product>> GetProducts(bool isIncludeRelative = true);
    }
}
