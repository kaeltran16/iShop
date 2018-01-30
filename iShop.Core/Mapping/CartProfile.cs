namespace iShop.Core.Mapping
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
