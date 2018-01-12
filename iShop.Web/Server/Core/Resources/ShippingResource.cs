using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Commons.Helpers;

namespace iShop.Web.Server.Core.Resources
{
    public class ShippingResource
    {
        public Guid Id { get; set; }
        public DateTime ShippingDate { get; set; }
        public ShippingState ShippingState { get; set; }
        
        public double Charge { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public ShippingResource()
        {
            ShippingState = ShippingState.None;
        }
    }
}
