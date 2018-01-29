using System;
using System.Collections.Generic;

namespace iShop.Core.DTOs
{
    public class ShoppingCartResource
    {
        public Guid Id { get; set; }
        public ApplicationUserResource User { get; set; }
        public ICollection<CartResource> Carts { get; set; }
        public DateTime PlacedDate { get; set; }
    }
}
