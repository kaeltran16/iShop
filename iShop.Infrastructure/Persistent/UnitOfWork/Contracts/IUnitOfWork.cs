using System;
using System.Threading.Tasks;
using iShop.Infrastructure.Persistent.Repositories.Contracts;

namespace iShop.Infrastructure.Persistent.UnitOfWork.Contracts
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
