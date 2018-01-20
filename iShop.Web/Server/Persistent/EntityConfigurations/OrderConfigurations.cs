using iShop.Web.Server.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Web.Server.Persistent.EntityConfigurations
{
    public class OrderConfigurations: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {      
            builder
                .HasOne(s => s.Shipping)
                .WithOne(o => o.Order)
                .HasForeignKey<Shipping>(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(s => s.Invoice)
                .WithOne(o => o.Order)
                .HasForeignKey<Invoice>(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
