using iShop.Web.Server.Commons.BaseClasses;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;

namespace iShop.Web.Server.Mapping
{
    public class ApplicationUserProfile:BaseProfile
    {

        protected override void CreateMap()
        {
            CreateMap<ApplicationUser, ApplicationUserResource>();
            CreateMap<ApplicationUserResource, ApplicationUser>()
                .ForMember(a=>a.Id, opt=>opt.Ignore())
                .ForAllMembers(opt => opt.Condition(
                    (source, destination, sourceMember, destMember) => (sourceMember != null)));
                
            CreateMap<RegisterResource, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterResource>();
        }
    }
}
