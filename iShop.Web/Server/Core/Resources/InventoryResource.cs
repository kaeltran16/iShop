using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class InventoryResource
    {
        public ProductResource Product { get; set; }
        public SupplierResource Supplier { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
