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
        [Required]
        public DateTime ShippingDate { get; set; }
        public ShippingState ShippingState { get; set; }
        [Required]
        public double Charge { get; set; }
        [Required]
        [StringLength(50)]
        public string Ward { get; set; }
        [Required]
        [StringLength(50)]
        public string Disctrict { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }


        public ShippingResource()
        {
            ShippingState = ShippingState.None;
        }
    }
}
