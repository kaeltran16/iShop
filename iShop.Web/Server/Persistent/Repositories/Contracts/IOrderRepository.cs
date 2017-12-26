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

        Task<Order> GetOrder(int shoppingCartId, bool includeRelated = true);
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrderOfUser(string userId);


    }
}
