namespace iShop.Core.Mapping
{
    public class ShippingProfile:BaseProfile
    {
    

        protected override void CreateMap()
        {
            CreateMap<Shipping, ShippingResource>();

            CreateMap<ShippingResource, Shipping>()
                .ForMember(s => s.Id, opt => opt.Ignore())
                .ForMember(s => s.Order, opt => opt.Ignore());

        }
    }
}
