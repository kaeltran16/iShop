using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iShop.Web.Server.Core.Resources
{
    public class SupplierResource
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<InventoryResource> Inventories { get; set; }
    }
}