using iShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Infrastructure.Persistent.EntityConfigurations
{
    public class ImageConfigurations: IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(i => i.ProductId).IsRequired();
            builder.Property(i => i.FileName).IsRequired();
        }
    }
}
