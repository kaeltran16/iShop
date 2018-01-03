using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public ICollection<Product> Products { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }

        public Cart()
        {
            Id = Guid.NewGuid();
            Products = new Collection<Product>();
        }
    }
}
