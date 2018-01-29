using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iShop.Core.DTOs
{
    public class SavedOrderResource
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public ICollection<OrderedItemResource> OrderedItems { get; set; }
        public SavedOrderResource()
        {
            OrderedItems = new Collection<OrderedItemResource>();
        }
    }
}
