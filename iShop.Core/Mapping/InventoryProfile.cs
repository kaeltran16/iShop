using iShop.Core.DTOs;
using iShop.Core.Entities;

namespace iShop.Core.Mapping
{
    public class InventoryProfile:BaseProfile
    {
    

        protected override void CreateMap()
        {
            CreateMap<Inventory, InventoryDto>();
            CreateMap<InventoryDto, Inventory>();
        }
    }
}
