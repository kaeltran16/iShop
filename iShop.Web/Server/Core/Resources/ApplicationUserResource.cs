using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class ApplicationUserResource
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public ICollection<ShoppingCartResource> ShoppingCarts { get; set; }
        public ICollection<OrderResource> Orders { get; set; }
    }
}
