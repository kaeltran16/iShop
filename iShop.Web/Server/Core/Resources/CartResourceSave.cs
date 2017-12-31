using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class CartResourceSave
    {
        public Guid Id { get; set; }
  
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCartResourceSave ShoppingCart { get; set; }

    }
}
