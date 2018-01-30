namespace iShop.Core.Mapping
{
    public class OrderItemProfile:BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<OrderedItem, OrderedItemResource>();
            CreateMap<OrderedItemResource, OrderedItem>();
        }
    }
}
