using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Core.Models;

namespace iShop.Web.Server.Core.Resources
{
    public class SavedOrderResource
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ShippingResource Shipping { get; set; }
        public ICollection<OrderedItemResource> OrderedItems { get; set; }
        public SavedOrderResource()
        {
            OrderedItems = new Collection<OrderedItemResource>();
        }
    }
}
