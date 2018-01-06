using iShop.Web.Server.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Web.Server.Persistent.EntityConfigurations
{
    public class InvoiceConfigurations: IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(i => i.OrderId)
                .IsRequired();

            builder
                .HasOne(s => s.Order)
                .WithOne(o => o.Invoice)
                .HasForeignKey<Invoice>(o=>o.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.OrderId)
                .IsRequired();
        }
    }
}
