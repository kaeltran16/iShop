using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Commons;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace iShop.Web.Server.Persistent.Repositories.Commons
{
    public class OrderRepository : DataRepositoryBase<Order>, IOrderRepository
    {

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrders(bool isIncludeRelative = true)
        {
            return isIncludeRelative
                ? await GetAllAsync(includeProperties: source => source
                    .Include(o => o.OrderedItems)
                    .Include(o => o.Shipping)
                    .Include(o => o.Invoice)
                    .Include(o => o.User))
                : await GetAllAsync();
        }

        public async Task<IEnumerable<Order>> GetUserOrders(Guid userId, bool isIncludeRelative = true)
        {
            Expression<Func<Order, bool>> predicate = o => o.UserId == userId;

            return isIncludeRelative
                ? await GetAllAsync(
                    predicate: predicate,
                    includeProperties: source => source
                        .Include(o => o.OrderedItems)
                        .Include(o => o.Shipping)
                        .Include(o => o.Invoice)
                        .Include(o => o.User))
                : await GetAllAsync(predicate);
        }

        public async Task<Order> GetOrder(Guid orderId, bool isIncludeRelative = true)
        {
            Expression<Func<Order, bool>> predicate = o => o.Id == orderId;

            return isIncludeRelative
                ? await GetSingleAsync(predicate,
                    includeProperties: source => source
                        .Include(o => o.OrderedItems)
                        .Include(o => o.Shipping)
                        .Include(o => o.Invoice)
                        .Include(o => o.User))
                : await GetSingleAsync(predicate);
        } 
    }
}

