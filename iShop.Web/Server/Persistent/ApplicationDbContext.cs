using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // if a Shopping Cart is deleted, those carts assosiate with it are deleted as well
            modelBuilder.Entity<ShoppingCart>().HasMany(c => c.Carts).WithOne(s => s.ShoppingCart)
                .OnDelete(DeleteBehavior.Cascade);
            // prevent EF from deleting the Product when the Cart is deleted
            modelBuilder.Entity<Cart>().HasMany(p => p.Products).WithOne(c => c.Cart).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>().HasMany(i => i.Images).WithOne(p => p.Product)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>().HasMany(i => i.OrderedItems).WithOne(o => o.Order)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Orders).WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.ShoppingCarts).WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Supplier>().HasMany(s => s.Inventories).WithOne(i => i.Supplier)
                .OnDelete(DeleteBehavior.Cascade);
            // One to one relationship
            modelBuilder.Entity<Product>().HasOne(c => c.Category).WithOne(c => c.Product)
                .OnDelete(DeleteBehavior.SetNull);
            

            modelBuilder.Entity<Inventory>().HasOne(p => p.Product).WithOne(i => i.Inventory)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>().HasOne(s => s.Shipping).WithOne(o => o.Order).IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>().HasOne(s => s.Shipping).WithOne(o => o.Order)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invoice>().HasOne(s => s.Order).WithOne(o => o.Invoice)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(e => e.ToTable("User"));
            modelBuilder.Entity<ApplicationRole>(e => e.ToTable("Role"));
            modelBuilder.Entity<IdentityUserRole<Guid>>(e => e.ToTable("UserRole"));
            modelBuilder.Entity<IdentityUserClaim<Guid>>(e => e.ToTable("UserClaim"));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(e => e.ToTable("UserLogin"));

          
        }
    }
}
