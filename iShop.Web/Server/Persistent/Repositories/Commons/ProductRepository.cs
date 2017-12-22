using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace iShop.Web.Server.Persistent.Repositories.Commons
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Product> GetProductId(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.Products.FindAsync(id);

            return await _context.Products
                .Include(c => c.Category)
                .Include(p => p.Image)
                .SingleOrDefaultAsync(v => v.Id == id);
        }
        public async Task<IEnumerable<Product>> GetProduct()
        {


            return await _context.Products
                .Include(c => c.Category)
                .Include(p => p.Image)
                .ToListAsync();
        }



        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Remove(Product product)
        {
            _context.Remove(product);
        }
    }
}
