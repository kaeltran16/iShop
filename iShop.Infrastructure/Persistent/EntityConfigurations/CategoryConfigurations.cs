using iShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Infrastructure.Persistent.EntityConfigurations
{
    public class CategoryConfigurations:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired();
        }
    }
}
