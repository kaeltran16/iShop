using iShop.Web.Server.Commons.BaseClasses;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;

namespace iShop.Web.Server.Mapping
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
