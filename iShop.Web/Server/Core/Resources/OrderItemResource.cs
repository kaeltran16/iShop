using System.ComponentModel.DataAnnotations;

namespace iShop.Web.Server.Core.Resources
{
    public class OrderItemResource
    {
        public ProductResource Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        public OrderResource Order { get; set; }
    }
}