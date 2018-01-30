using System;
using System.Collections.Generic;

namespace iShop.Core.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public ICollection<OrderedItemDto> OrderedItems { get; set; }
        public ShippingDto Shipping { get; set; }
        public InvoiceDto Invoice { get; set; }
    }
}
