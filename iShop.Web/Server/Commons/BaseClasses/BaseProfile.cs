using AutoMapper;
using iShop.Web.Server.Core.Models;

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
