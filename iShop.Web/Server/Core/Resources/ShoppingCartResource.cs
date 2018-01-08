using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class ShoppingCartResource
    {
        public Guid Id { get; set; }
        public ApplicationUserResource User { get; set; }
        public ICollection<CartResource> Carts { get; set; }
        [Required]
        public DateTime PlacedDate { get; set; }
    }
}
