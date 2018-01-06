using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Web.Server.Persistent.EntityConfigurations
{
    public class OrderedItemConfigurations: IEntityTypeConfiguration<OrderedItem>
    {
        public void Configure(EntityTypeBuilder<OrderedItem> builder)
        {
            builder
                .HasKey(oi => new {oi.ProductId, oi.OrderId});

            builder
                .Property(oi => oi.ProductId)
                .IsRequired();

            builder
                .Property(oi => oi.OrderId)
                .IsRequired();
            builder
                .Property(oi => oi.Quantity)
                .IsRequired();

            builder
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderedItems)
                .HasForeignKey(ot => ot.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(oi => oi.Order)
                .WithMany(p => p.OrderedItems)
                .HasForeignKey(ot => ot.OrderId)
                .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
