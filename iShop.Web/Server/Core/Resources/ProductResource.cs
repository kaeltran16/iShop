using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class ProductResource
    {
        public Guid Id { get; set; }
        public ICollection<InventoryResource> Inventories { get; set; }
        public ICollection<CartResource> Carts { get; set; }
        public ICollection<CategoryResource> Categories { get; set; }

        public string Sku { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string Summary { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }
//        public ICollection<ImageResource> Images { get; set; }
    }
}
