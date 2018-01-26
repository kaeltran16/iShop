using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class ProductResource
    {
        public Guid Id { get; set; }      
        public string Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Summary { get; set; }
        public DateTime ExpiredDate { get; set; }
        public Guid SupplierId { get; set; }
        public InventoryResource Inventory { get; set; }
        public ICollection<CategoryResource> Categories { get; set; }
        public ICollection<ImageResource> Images { get; set; }

        public ProductResource()
        {
            Categories = new Collection<CategoryResource>();
            Images = new Collection<ImageResource>();
        }
    }
}
