using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Web.Server.Persistent.EntityConfigurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasMany(a => a.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o=>o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(a => a.ShoppingCarts)
                .WithOne(sc => sc.User)
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(f => f.FirstName)
                .IsRequired();

            builder.Property(f => f.LastName)
                .IsRequired();

            builder.Property(f => f.Street)
                .IsRequired();

            builder.Property(f => f.City)
                .IsRequired();
        }
    }
}
