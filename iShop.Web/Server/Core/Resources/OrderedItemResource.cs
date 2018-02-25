using System;
using System.ComponentModel.DataAnnotations;
using iShop.Web.Server.Core.Models;

namespace iShop.Web.Server.Core.Resources
{
    public class OrderedItemResource
    {
        public ProductResource Product { get; set; }
        public Guid ProductId { get; set; }
        [Required] 
        public int Quantity { get; set; }

    }
}