using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Contracts;
using iShop.Web.Server.Core.Models;

namespace iShop.Web.Server.Persistent.Repositories.Contracts
{
    public interface IOrderRepository : IDataRepository<Order>
    {
        Task<Order> GetOrder(Guid userId, Guid shoppingCartId, bool includeRelated = true);
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetUserOrders(Guid userId);
    }
}
