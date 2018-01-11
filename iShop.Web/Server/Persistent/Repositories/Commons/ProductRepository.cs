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
    public class ProductRepository : DataRepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<Product> GetProduct(Guid id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.Products.FindAsync(id);

            return await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(c => c.Category)
                .Include(p => p.Images)
                .Include(p => p.Inventories)
                .ThenInclude(i=>i.Supplier)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(c => c.Category)
                .Include(p => p.Images)
                .Include(p => p.Inventories)
                .ThenInclude(i => i.Supplier)
                .ToListAsync();
        }

    }
}
