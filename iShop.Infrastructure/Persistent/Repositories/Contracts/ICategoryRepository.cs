using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Core.Entities;
using iShop.Infrastructure.Data;

namespace iShop.Infrastructure.Persistent.Repositories.Contracts
{
    public interface ICategoryRepository: IDataRepository<Category>
    {
        Task<Category> GetCategory(Guid id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
