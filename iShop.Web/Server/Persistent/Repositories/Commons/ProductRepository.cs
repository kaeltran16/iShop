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
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetProductId(Guid id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.Products.FindAsync(id);

            return await _context.Products
                .Include(c => c.Category)
                .Include(p => p.Image)
                .SingleOrDefaultAsync(v => v.Id == id);
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products
                .Include(c => c.Category)
                .Include(p => p.Image)
                .ToListAsync();
        }

    }
}
