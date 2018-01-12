using System;

namespace iShop.Web.Server.Core.Models
{
    public class Inventory
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int Stock { get; set; }
    }
}
