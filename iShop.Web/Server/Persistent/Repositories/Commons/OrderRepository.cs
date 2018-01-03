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

        //public async Task<IEnumerable<Order>> GetUserOrders(Guid userId)
        //{
        //    return await _context.Orders
        //        .Include(c => c.ShoppingCart)
        //        .ThenInclude(p => p.Carts)
        //        .ThenInclude(p => p.Product)
        //        .Include(c => c.ShoppingCart)
        //        .ThenInclude(u => u.UserId)
        //        .Where(s => s.ShoppingCartId == userId).ToListAsync();
        //}

        //public async Task<Order> GetOrder(Guid userId, Guid shoppingCartId, bool includeRelated = true)
        //{
        //    if (!includeRelated)
        //        return await _context.Orders.FindAsync(shoppingCartId);

        //    return await _context.Orders.Include(c => c.ShoppingCart)
        //        .ThenInclude(p => p.Carts)
        //        .ThenInclude(p => p.Product)
        //        .Include(u => u.User)
        //        .SingleOrDefaultAsync(v => v.ShoppingCartId == shoppingCartId);
        //}
        //public async Task<IEnumerable<Order>> GetOrders()
        //{
        //    return await _context.Orders
        //        .Include(c => c.ShoppingCart)
        //        .ThenInclude(p => p.Carts)
        //        .ThenInclude(p => p.Product)
        //        .Include(u => u.User)
        //        .ToListAsync();
        //}

    }
}

