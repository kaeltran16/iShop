using iShop.Web.Server.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Web.Server.Persistent.EntityConfigurations
{
    public class ProductCategoryConfigurations: IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(pc => new {pc.CategoryId, pc.ProductId});
            builder.Property(pc => pc.CategoryId).IsRequired();
            builder.Property(pc => pc.ProductId).IsRequired();
            builder
                .HasOne(c => c.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
