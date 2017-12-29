using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace iShop.Web.Server.Core.Resources
{
    public class ShoppingCartResource
    {
      
        public Guid Id { get; set; }
   
        public string UserId { get; set; }
        public ApplicationUserResources User { get; set; }


        public DateTime PlacedDate { get; set; }
        public ICollection<CartResource> Carts { get; set; }

        public ShoppingCartResource()
        {
            Carts = new Collection<CartResource>();
        }
    }
}
