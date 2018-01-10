using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class SavedOrderResource
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ShippingId { get; set; }
        public Guid InvoiceId { get; set; }
        public ICollection<Guid> OrderedItems { get; set; }

        public SavedOrderResource()
        {
            OrderedItems = new Collection<Guid>();
        }
    }
}
