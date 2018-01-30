using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Core.Entities;
using iShop.Infrastructure.Data;

namespace iShop.Infrastructure.Persistent.Repositories.Contracts
{
    public interface ISupplierRepository: IDataRepository<Supplier>
    {
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<Supplier> GetSupplier(Guid supplierId);

    }
}
