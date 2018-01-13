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
        [StringLength(50)]
        public string Sku { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [StringLength(255)]
        public string Summary { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }
        public Guid SupplierId { get; set; }
        public InventoryResource Inventory { get; set; }
        public ICollection<CategoryResource> Categories { get; set; }
        public ICollection<ImageResource> Images { get; set; }
    }
}
