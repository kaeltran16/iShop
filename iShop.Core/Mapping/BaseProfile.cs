using AutoMapper;

namespace iShop.Core.Mapping
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
