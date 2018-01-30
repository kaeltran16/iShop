namespace iShop.Core.Mapping
{
    public class ApplicationUserProfile:BaseProfile
    {

        protected override void CreateMap()
        {
            CreateMap<ApplicationUser, ApplicationUserResource>();
            CreateMap<ApplicationUserResource, ApplicationUser>();
            CreateMap<RegisterResource, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterResource>();
        }
    }
}
