using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Persistent.Repositories.Contracts;

namespace iShop.Web.Server.Persistent.UnitOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        //We will not change these interfaces inside here so get is far more enough
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IOrderRepository OrderRepository { get; }
        IImagesRepository ImageRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IShippingRepository ShippingRepository { get; }
        Task<bool> CompleteAsync();
    }

}
