using iShop.Core.DTOs;
using iShop.Core.Entities;

namespace iShop.Core.Mapping
{
    public class CategoryProfile: BaseProfile
    {
  

        protected override void CreateMap()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<CategoryDto, Category>()
                .ForMember(cr => cr.Id, opt => opt.Ignore())
                .ForMember(c => c.ProductCategories, opt => opt.Ignore());
        }
    }
}
