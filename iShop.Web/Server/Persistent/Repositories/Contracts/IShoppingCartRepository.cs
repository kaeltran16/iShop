using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Contracts;
using iShop.Web.Server.Core.Models;

namespace iShop.Web.Server.Persistent.Repositories.Contracts
{
    public interface IShoppingCartRepository: IDataRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetShoppingCart(Guid id, bool includeRelated = true);
        Task<IEnumerable<ShoppingCart>> GetShoppingCarts();
        Task<IEnumerable<ShoppingCart>> GetUserShoppingCarts(Guid userId);

    }
}
