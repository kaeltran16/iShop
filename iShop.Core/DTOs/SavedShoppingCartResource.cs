using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iShop.Core.DTOs
{
    public class SavedShoppingCartResource
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<CartResource> Carts { get; set; }

        public SavedShoppingCartResource()
        {
            Carts = new Collection<CartResource>();
        }
    }
}
