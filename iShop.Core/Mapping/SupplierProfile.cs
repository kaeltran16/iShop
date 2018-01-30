namespace iShop.Core.Mapping
{
    public class SupplierProfile : BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Supplier, SupplierResource>();
            CreateMap<SupplierResource, Supplier>()
                .ForMember(cr => cr.Id, opt => opt.Ignore())
                .ForMember(c => c.Inventories, opt => opt.Ignore());
        }
    }
}
