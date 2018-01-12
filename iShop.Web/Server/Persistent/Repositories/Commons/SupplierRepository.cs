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
    public class SupplierRepository: DataRepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier> GetSupplier(Guid supplierId)
        {
            return await _context.Suppliers.SingleOrDefaultAsync(i => i.Id == supplierId);
        }
    }
}
