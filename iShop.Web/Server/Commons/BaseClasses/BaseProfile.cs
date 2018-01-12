using AutoMapper;

namespace iShop.Web.Server.Commons.BaseClasses
{
    public abstract class BaseProfile: Profile
    {
        protected BaseProfile(string profileName)
        {
            ProfileName = profileName;
            CreateMap();
        }

        public override string ProfileName { get; }

        protected abstract void CreateMap();
    }
}
