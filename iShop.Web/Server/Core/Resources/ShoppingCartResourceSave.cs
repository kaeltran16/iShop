using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class ShoppingCartResourceSave
    {
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public DateTime PlacedDate { get; set; }
        public ICollection<CartResourceSave> Carts { get; set; }
        public ShoppingCartResourceSave()
        {
            Carts = new Collection<CartResourceSave>();
        }
    }
}
