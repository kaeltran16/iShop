using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Core.Entities;
using iShop.Infrastructure.Data;

namespace iShop.Infrastructure.Persistent.Repositories.Contracts
{
    public interface IShoppingCartRepository : IDataRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetShoppingCart(Guid id, bool isIncludeRelative = true);
        Task<IEnumerable<ShoppingCart>> GetShoppingCarts(bool isIncludeRelative = true);
        Task<IEnumerable<ShoppingCart>> GetUserShoppingCarts(Guid userId, bool isIncludeRelative = true);

    }
}
