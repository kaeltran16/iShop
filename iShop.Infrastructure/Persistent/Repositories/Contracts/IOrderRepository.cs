using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Core.Entities;
using iShop.Infrastructure.Data;

namespace iShop.Infrastructure.Persistent.Repositories.Contracts
{
    public interface IOrderRepository : IDataRepository<Order>
    {
        Task<Order> GetOrder(Guid orderId, bool isIncludeRelative = true);
        Task<IEnumerable<Order>> GetOrders(bool isIncludeRelative = true);
        Task<IEnumerable<Order>> GetUserOrders(Guid userId, bool isIncludeRelative = true);
    }
}
