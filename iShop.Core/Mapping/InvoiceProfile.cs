using iShop.Core.DTOs;
using iShop.Core.Entities;

namespace iShop.Core.Mapping
{
    public class InvoiceProfile:BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>()
                .ForMember(sr => sr.Id, opt => opt.Ignore());

        }
    }
}
