using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Core.Entities;
using iShop.Infrastructure.Data;
using iShop.Infrastructure.Persistent.Repositories.Contracts;

namespace iShop.Infrastructure.Persistent.Repositories.Commons
{
    public class SupplierRepository: DataRepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await GetAllAsync();
        }

        public async Task<Supplier> GetSupplier(Guid supplierId)
        {
            return await GetSingleAsync(i => i.Id == supplierId);
        }
    }
}
