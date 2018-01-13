using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Web.Server.Commons.BaseClasses;

namespace iShop.Web.Server.Core.Models
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
