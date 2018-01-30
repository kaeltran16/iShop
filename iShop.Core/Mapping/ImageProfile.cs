namespace iShop.Core.Mapping
{
    public class ImageProfile:BaseProfile
    {
     

        protected override void CreateMap()
        {
            CreateMap<Image, ImageResource>();
            CreateMap<ImageResource, Image>();
        }
    }
}
