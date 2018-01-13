using iShop.Web.Server.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Web.Server.Persistent.EntityConfigurations
{
    public class ShippingConfigurations: IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder
                .Property(s => s.OrderId)
                .IsRequired();

            builder
                .Property(s => s.Charge)
                .IsRequired();

            builder
                .Property(s => s.City)
                .IsRequired();

            builder
                .Property(s => s.Disctrict)
                .IsRequired();
            builder
                .Property(s => s.Ward)
                .IsRequired();

            builder
                .HasOne(s => s.Order)
                .WithOne(o => o.Shipping)
                .HasForeignKey<Shipping>(s => s.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
