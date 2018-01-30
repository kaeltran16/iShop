namespace iShop.Core.Mapping
{
    public class CategoryProfile: BaseProfile
    {
  

        protected override void CreateMap()
        {
            CreateMap<Category, CategoryResource>();

            CreateMap<CategoryResource, Category>()
                .ForMember(cr => cr.Id, opt => opt.Ignore())
                .ForMember(c => c.ProductCategories, opt => opt.Ignore());
        }
    }
}
