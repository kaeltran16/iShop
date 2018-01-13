using iShop.Web.Server.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Web.Server.Persistent.EntityConfigurations
{
    public class CartConfigurations: IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .HasKey(c => new {c.ProductId, c.ShoppingCartId});

            builder
                .Property(c => c.Quantity)
                .IsRequired();

            builder
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.ShoppingCart)
                .WithMany(sc => sc.Carts)
                .HasForeignKey(c => c.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
