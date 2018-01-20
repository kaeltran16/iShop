using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
                .ForMember(s => s.Id, opt => opt.Ignore());

            //.AfterMap((sr, s) => { s.Order.ShippingId = sr.Id; });

        }
    }
}
