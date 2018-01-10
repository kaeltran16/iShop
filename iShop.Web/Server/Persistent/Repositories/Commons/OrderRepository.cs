using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Commons;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace iShop.Web.Server.Persistent.Repositories.Commons
{
    public class OrderRepository : DataRepositoryBase<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(Guid userId)
        {
            return await _context.Orders
                .Include(o => o.OrderedItems)
                .Include(o => o.Shipping)
                .Include(o => o.Invoice)
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order> GetOrder(Guid orderId, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.Orders.FindAsync(orderId);

            return await _context.Orders
                .Include(o => o.OrderedItems)
                .Include(o => o.Shipping)
                .Include(o => o.Invoice)
                .Include(o => o.User)
                .SingleOrDefaultAsync(o => o.Id == orderId);
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders
                .Include(o => o.OrderedItems)
                .Include(o => o.Shipping)
                .Include(o => o.Invoice)
                .Include(o => o.User)
                .ToListAsync();
        }

    }
}

