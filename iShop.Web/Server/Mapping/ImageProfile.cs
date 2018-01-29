using iShop.Web.Server.Commons.BaseClasses;

namespace iShop.Web.Server.Mapping
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
