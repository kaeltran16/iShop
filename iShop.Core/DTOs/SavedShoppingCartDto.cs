using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iShop.Core.DTOs
{
    public class SavedShoppingCartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<CartDto> Carts { get; set; }

        public SavedShoppingCartDto()
        {
            Carts = new Collection<CartDto>();
        }
    }
}
