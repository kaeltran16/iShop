using iShop.Web.Server.Core.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class CartResource
    {
        public Guid Id { get; set; }
   
        public int Quantity { get; set; }
        public ProductResource Product { get; set; }
    }
}
