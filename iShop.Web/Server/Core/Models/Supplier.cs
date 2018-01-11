using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid InventoryId { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
        public Supplier()
        {
            Id = Guid.NewGuid();
            Inventories=new Collection<Inventory>();
        }
    }
}
