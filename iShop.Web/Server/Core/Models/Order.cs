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
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
        [Required]      
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public DateTime PlacedDate { get; set; }

        public Order()
        {
            PlacedDate = DateTime.Now;
        }
    }
}
