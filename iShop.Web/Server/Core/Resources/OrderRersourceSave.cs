using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class OrderRersourceSave
    {
        public string UserId { get; set; }
      


        public int ShoppingCartId { get; set; }
      
        public DateTime PlacedDate { get; set; }

        public OrderRersourceSave()
        {
            PlacedDate = DateTime.Now;
        }
    }
}
