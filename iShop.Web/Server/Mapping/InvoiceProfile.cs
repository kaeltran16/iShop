using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Commons.BaseClasses;

namespace iShop.Web.Server.Mapping
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
