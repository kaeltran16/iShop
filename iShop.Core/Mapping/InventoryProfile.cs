namespace iShop.Core.Mapping
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
