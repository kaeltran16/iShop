using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace iShop.Web.Server.Core.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; }      
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ApplicationUser()
        {
            CreatedDate = DateTime.Now;
            ShoppingCarts = new Collection<ShoppingCart>();
            Orders = new Collection<Order>();
        }
    }
}
