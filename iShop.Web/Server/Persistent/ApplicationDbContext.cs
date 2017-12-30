using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iShop.Web.Server.Persistent
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {   
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(g => new { g.UserId, g.ShoppingCartId });
            base.OnModelCreating(builder);
        }
    }
}
