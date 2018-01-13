using System;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace iShop.Web.Server.Commons.Extensions
{
    public static class ApplicationDbContextExtensions
    {
        public static void ApplyEntityConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfigurations());
            modelBuilder.ApplyConfiguration(new CategoryConfigurations());
            modelBuilder.ApplyConfiguration(new CartConfigurations());
            modelBuilder.ApplyConfiguration(new InventoryConfigurations());
            modelBuilder.ApplyConfiguration(new InvoiceConfigurations());
            modelBuilder.ApplyConfiguration(new ImageConfigurations());
            modelBuilder.ApplyConfiguration(new OrderConfigurations());
            modelBuilder.ApplyConfiguration(new OrderedItemConfigurations());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new ShippingConfigurations());
            modelBuilder.ApplyConfiguration(new ShoppingCartConfigurations());
            modelBuilder.ApplyConfiguration(new SupplierConfigurations());
        }

        public static void ChangeIdentityTableNames(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(e => e.ToTable("User"));
            modelBuilder.Entity<ApplicationRole>(e => e.ToTable("Role"));
            modelBuilder.Entity<IdentityUserRole<Guid>>(e => e.ToTable("UserRole"));
            modelBuilder.Entity<IdentityUserClaim<Guid>>(e => e.ToTable("UserClaim"));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(e => e.ToTable("UserLogin"));
        }
    }
}
