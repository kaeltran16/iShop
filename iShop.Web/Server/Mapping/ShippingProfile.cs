using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Commons.BaseClasses;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;

namespace iShop.Web.Server.Mapping
{
    public class ShippingProfile:BaseProfile
    {
    

        protected override void CreateMap()
        {
            CreateMap<Shipping, ShippingResource>();

            CreateMap<ShippingResource, Shipping>()
                .ForMember(sr => sr.Id, opt => opt.Ignore());
        }
    }
}
