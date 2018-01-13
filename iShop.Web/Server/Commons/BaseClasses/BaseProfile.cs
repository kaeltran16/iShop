using AutoMapper;

namespace iShop.Web.Server.Commons.BaseClasses
{
    public abstract class BaseProfile : Profile
    {
        protected BaseProfile()
        {
            CreateMap();
        }




        protected abstract void CreateMap();
    }
}
