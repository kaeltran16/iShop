using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public DateTime PlacedDate { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public string UserId  { get; set; }
        public ApplicationUser User { get; set; }
        public ShoppingCart()
        {
            Carts = new Collection<Cart>();
        }
    }
}
