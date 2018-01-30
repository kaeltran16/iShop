using iShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Infrastructure.Persistent.EntityConfigurations
{
    public class SupplierConfigurations: IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder
                .Property(s => s.Name)
                .IsRequired();
        }
    }
}
