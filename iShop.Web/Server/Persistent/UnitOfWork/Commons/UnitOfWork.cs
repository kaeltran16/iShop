using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Persistent.Repositories.Commons;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;

namespace iShop.Web.Server.Persistent.UnitOfWork.Commons
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IShoppingCartRepository _shoppingCartRepository;
        private IOrderRepository _orderRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // Getter for repository interface. When call these getter, if the repository is null then create a new one
        // otherwise return it
        public ICategoryRepository CategoryRepository =>
            _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context));
        public IProductRepository ProductRepository =>
            _productRepository ?? (_productRepository = new ProductRepository(_context));
        public IShoppingCartRepository ShoppingCartRepository =>
            _shoppingCartRepository ?? (_shoppingCartRepository = new ShoppingCartRepository(_context));
        //public IOrderRepository OrderRepository =>
        //    _orderRepository ?? (_orderRepository = new OrderRepository(_context));
        // Complete current unit of work, save changes to the database
        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
