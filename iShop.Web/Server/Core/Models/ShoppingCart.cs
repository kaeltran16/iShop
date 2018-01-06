using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public DateTime PlacedDate { get; set; }
       
        public ShoppingCart()
        {
            Id = Guid.NewGuid();
            Carts = new Collection<Cart>();
            PlacedDate = DateTime.Now;     
        }
    }
}
