using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class OrderResource
    {

   
        public Guid UserId { get; set; }
        public ApplicationUserResources User { get; set; }
  


        public int ShoppingCartId { get; set; }
        public ShoppingCartResource ShoppingCart { get; set; }
        public DateTime PlacedDate { get; set; }

        public OrderResource()
        {
            PlacedDate = DateTime.Now;
        }
    }
}
