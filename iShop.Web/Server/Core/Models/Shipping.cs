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
        public string District { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName  { get; set; }
        public Shipping()
        {
            ShippingState = ShippingState.None;
        }
    }
}
