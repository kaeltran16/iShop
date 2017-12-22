using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class ShoppingCartResourceSave
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime PlacedDate { get; set; }
        public ICollection<CartResourceSave> Carts { get; set; }
        public ShoppingCartResourceSave()
        {
            Carts = new Collection<CartResourceSave>();
        }
    }
}
