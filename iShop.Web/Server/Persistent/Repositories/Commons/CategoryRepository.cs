using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Commons;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace iShop.Web.Server.Persistent.Repositories.Commons
{
    public class CategoryRepository : DataRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetCategory(Guid id)
        {
            return await _context.Categories
                .Include(g => g.ProductCategories)
                .ThenInclude(g => g.Product)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories
                .Include(g=>g.ProductCategories)
                .ThenInclude(p=>p.Product)
                .ToListAsync();
        }


    }
}
