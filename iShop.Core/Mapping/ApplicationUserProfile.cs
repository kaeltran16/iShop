using iShop.Core.DTOs;
using iShop.Core.Entities;

namespace iShop.Core.Mapping
{
    public class ApplicationUserProfile:BaseProfile
    {

        protected override void CreateMap()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserDto, ApplicationUser>();
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterDto>();
        }
    }
}
