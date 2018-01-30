namespace iShop.Core.Mapping
{
    public class InvoiceProfile:BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Invoice, InvoiceResource>();
            CreateMap<InvoiceResource, Invoice>()
                .ForMember(sr => sr.Id, opt => opt.Ignore());

        }
    }
}
