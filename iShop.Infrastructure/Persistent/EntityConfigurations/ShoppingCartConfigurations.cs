using iShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Infrastructure.Persistent.EntityConfigurations
{
    public class ShoppingCartConfigurations: IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            
        }
    }
}
