using System;
using iShop.Web.Server.Commons.BaseClasses;
using iShop.Web.Server.Commons.Helpers;

namespace iShop.Web.Server.Core.Models
{
    public class Shipping : EntityBase
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime ShippingDate { get; set; }
        public ShippingState ShippingState { get; set; }
        public double Charge { get; set; }
        public string Ward { get; set; }
        public string Disctrict { get; set; }
        public string City { get; set; }

        public Shipping()
        {
            ShippingState = ShippingState.None;
        }
    }
}
