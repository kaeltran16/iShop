using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iShop.Core.Entities
{
    public class Supplier : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Inventory> Inventories { get; set; }

        public Supplier()
        {
            Inventories = new Collection<Inventory>();
        }
    }
}
