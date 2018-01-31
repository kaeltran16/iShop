using iShop.Core.DTOs;
using iShop.Core.Entities;

namespace iShop.Core.Mapping
{
    public class ShippingProfile:BaseProfile
    {
    

        protected override void CreateMap()
        {
            CreateMap<Shipping, ShippingDto>();

            CreateMap<ShippingDto, Shipping>()
                .ForMember(s => s.Id, opt => opt.Ignore())
                .ForMember(s => s.Order, opt => opt.Ignore());

        }
    }
}
