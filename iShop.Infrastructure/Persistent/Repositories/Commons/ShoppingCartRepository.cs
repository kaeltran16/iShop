using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Core.Entities;
using iShop.Infrastructure.Data;
using iShop.Infrastructure.Persistent.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace iShop.Infrastructure.Persistent.Repositories.Commons
{
    public class ShoppingCartRepository : DataRepositoryBase<ShoppingCart>, IShoppingCartRepository
    {

        public ShoppingCartRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ShoppingCart>> GetUserShoppingCarts(Guid userId, bool isIncludeRelative = true)
        {
            Expression<Func<ShoppingCart, bool>> predicate = p => p.UserId == userId;

            return isIncludeRelative
                ? await GetAllAsync(predicate,
                    includeProperties: src => src
                        .Include(c => c.Carts)
                        .ThenInclude(p => p.Product)
                        .Include(u => u.User))
                : await GetAllAsync(predicate);
        }

        public async Task<ShoppingCart> GetShoppingCart(Guid id, bool isIncludeRelative = true)
        {
            Expression<Func<ShoppingCart, bool>> predicate = p => p.Id == id;

            return isIncludeRelative
                ? await GetSingleAsync(predicate,
                     includeProperties: src => src
                    .Include(c => c.Carts)
                    .ThenInclude(p => p.Product)
                    .Include(u => u.User))
                : await GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<ShoppingCart>> GetShoppingCarts(bool isIncludeRelative = true)
        {
            return isIncludeRelative
                ? await GetAllAsync(includeProperties: src => src
                    .Include(c => c.Carts)
                    .ThenInclude(p => p.Product)
                    .Include(u => u.User))
                : await GetAllAsync();
        }
    }
}
