using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Contracts;

namespace iShop.Web.Server.Persistent.Repositories.Contracts
{
    public interface ICategoryRepository: IDataRepository<Category>
    {
        Task<Category> GetCategory(Guid id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
