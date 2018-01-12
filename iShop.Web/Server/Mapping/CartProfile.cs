using iShop.Web.Server.Commons.BaseClasses;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;

namespace iShop.Web.Server.Mapping
{
    public class CartProfile : BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Cart, CartResource>();
            CreateMap<CartResource, Cart>();
        }
    }
}
