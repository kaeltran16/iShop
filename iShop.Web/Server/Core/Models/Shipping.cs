using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models.Models.Enums;

namespace iShop.Web.Server.Core.Models
{
    public class Shipping
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime ShippingDate { get; set; }
        public ShippingState ShippingState { get; set; }
        public double Charge { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public Shipping()
        {
            Id = Guid.NewGuid();
            ShippingState = ShippingState.None;
        }
    }
}
