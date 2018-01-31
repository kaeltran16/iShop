using System.Linq;
using iShop.Core.DTOs;
using iShop.Core.Entities;

namespace iShop.Core.Mapping
{
    public class ShoppingCartProfile : BaseProfile
    {


        protected override void CreateMap()
        {
            CreateMap<ShoppingCart, SavedShoppingCartDto>();
            CreateMap<ShoppingCart, ShoppingCartDto>()
                .ForMember(or => or.Carts, opt => opt.MapFrom(p =>
                    p.Carts.Select(pc => new Cart() {ProductId = pc.ProductId, Quantity = pc.Quantity})));

            CreateMap<ShoppingCartDto, ShoppingCart>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.Carts, opt => opt.Ignore());

            CreateMap<SavedShoppingCartDto, ShoppingCart>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.Carts, opt => opt.MapFrom(c => c.Carts))
                .AfterMap((sr, s) =>
                {
                    var addedCarts = sr.Carts.Where(cr => s.Carts.All(c => c.ProductId != cr.ProductId))
                        .Select(cr => new Cart() { ProductId = cr.ProductId, Quantity = cr.Quantity, ShoppingCartId = sr.Id }).ToList();
                    foreach (var c in addedCarts)
                        s.Carts.Add(c);

                    var removedCartItems =
                        s.Carts.Where(oi => sr.Carts.Any(oir=>oir.ProductId!=oi.ProductId)).ToList();
                    foreach (var oi in removedCartItems)
                        s.Carts.Remove(oi);
                });
        }

    }
}
