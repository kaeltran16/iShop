using iShop.Web.Server.Commons.BaseClasses;

namespace iShop.Web.Server.Mapping
{
    public class InventoryProfile:BaseProfile
    {
    

        protected override void CreateMap()
        {
            CreateMap<Inventory, InventoryResource>();
            CreateMap<InventoryResource, Inventory>();
        }
    }
}
