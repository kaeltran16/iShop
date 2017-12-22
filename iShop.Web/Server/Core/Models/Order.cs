using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class Order
    {
        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        [Required]      
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public DateTime PlacedDate { get; set; }

        public Order()
        {
            PlacedDate = DateTime.Now;
        }
    }
}
