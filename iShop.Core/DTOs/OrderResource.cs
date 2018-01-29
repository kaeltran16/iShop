using System;
using System.Collections.Generic;

namespace iShop.Core.DTOs
{
    public class OrderResource
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public ICollection<OrderedItemResource> OrderedItems { get; set; }
        public ShippingResource Shipping { get; set; }
        public InvoiceResource Invoice { get; set; }
    }
}
