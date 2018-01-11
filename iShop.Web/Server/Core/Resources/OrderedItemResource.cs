using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Web.Server.Core.Resources
{
    public class OrderedItemResource
    {
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}