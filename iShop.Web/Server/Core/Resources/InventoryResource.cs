using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class InventoryResource
    {
        public Guid ProductId { get; set; }
        public int Stock { get; set; }
    }
}
