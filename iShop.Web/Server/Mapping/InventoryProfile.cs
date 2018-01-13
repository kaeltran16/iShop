using iShop.Web.Server.Commons.BaseClasses;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;

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
