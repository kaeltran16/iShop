using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iShop.Web.Server.Persistent.Repositories.Commons
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

//
//        public async Task<IEnumerable<ShoppingCart>> GetShoppingCartOfUser(string UserId)
//        {
//            return await _context.ShoppingCarts
//                .Include(c => c.Carts)
//                .ThenInclude(p => p.Product)
//                .Include(u => u.User)
//                .Where(s => s.UserId == UserId).ToListAsync();
//        }
//
//        public async Task<ShoppingCart> GetShoppingCart(int id, bool includeRelated = true)
//        {
//            if (!includeRelated)
//                return await _context.ShoppingCarts.FindAsync(id);
//
//            return await _context.ShoppingCarts.Include(c => c.Carts).ThenInclude(p => p.Product).Include(u => u.User)
//                .SingleOrDefaultAsync(v => v.Id == id);
//        }
//        public async Task<IEnumerable<ShoppingCart>> GetShoppingCarts()
//        {
//
//
//            return await _context.ShoppingCarts.Include(c => c.Carts).ThenInclude(p => p.Product).Include(u => u.User)
//
//                .ToListAsync();
//        }



        public void Add(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
        }

        public void Remove(ShoppingCart shoppingCart)
        {
            _context.Remove(shoppingCart);
        }


    }
}
