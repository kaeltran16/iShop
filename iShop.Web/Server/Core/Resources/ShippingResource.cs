using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models.Models.Enums;

namespace iShop.Web.Server.Core.Resources
{
    public class ShippingResource
    {
        public Guid Id { get; set; }
        public OrderResource Order { get; set; }
        [Required]
        public DateTime ShippingDate { get; set; }
        public ShippingState ShippingState { get; set; }
        [Required]
        public double Charge { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
    }
}
