using System.Linq;
using iShop.Web.Server.Commons.BaseClasses;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;

namespace iShop.Web.Server.Mapping
{
    public class ShoppingCartProfile : BaseProfile
    {


        protected override void CreateMap()
        {
            CreateMap<ShoppingCart, SavedShoppingCartResource>();
            CreateMap<ShoppingCart, ShoppingCartResource>()
                .ForMember(or => or.Carts, opt => opt.MapFrom(p =>
                    p.Carts.Select(pc => new Cart() {ProductId = pc.ProductId, Quantity = pc.Quantity})));

            CreateMap<ShoppingCartResource, ShoppingCart>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.Carts, opt => opt.Ignore());

            CreateMap<SavedShoppingCartResource, ShoppingCart>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.Carts, opt => opt.MapFrom(c => c.Carts))
                .AfterMap((sr, s) =>
                {
                    var addedCarts = sr.Carts.Where(cr => s.Carts.All(c => c.ProductId != cr.ProductId))
                        .Select(cr => new Cart() { ProductId = cr.ProductId, Quantity = cr.Quantity, ShoppingCartId = sr.Id }).ToList();
                    foreach (var c in addedCarts)
                        s.Carts.Add(c);

                    var removedCarts =
                        s.Carts.Where(c => sr.Carts.Any(cr => cr.ProductId != c.ProductId)).ToList();
                    foreach (var c in removedCarts)
                        s.Carts.Remove(c);
                });
        }

    }
}
