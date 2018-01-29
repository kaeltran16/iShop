using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iShop.Core.Entities
{
    public class Order : EntityBase
    {
        public Guid? UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Shipping Shipping { get; set; }

        public Invoice Invoice { get; set; }

        public ICollection<OrderedItem> OrderedItems { get; set; }

        public DateTime OrderedDate { get; set; }

        public Order()
        {
            OrderedItems = new Collection<OrderedItem>();
            OrderedDate = DateTime.Now;
        }
    }
}
