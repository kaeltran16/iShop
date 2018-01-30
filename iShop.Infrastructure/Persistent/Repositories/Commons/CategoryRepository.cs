using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Core.Entities;
using iShop.Infrastructure.Data;
using iShop.Infrastructure.Persistent.Repositories.Contracts;

namespace iShop.Infrastructure.Persistent.Repositories.Commons
{
    public class CategoryRepository : DataRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {        
        }

        public async Task<Category> GetCategory(Guid id)
        {
            return await GetSingleAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await GetAllAsync();
        }


    }
}
