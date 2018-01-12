using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class OrderResource
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<OrderedItemResource> OrderedItems { get; set; }
        public ShippingResource Shipping { get; set; }
        public InvoiceResource Invoice { get; set; }
    }
}
