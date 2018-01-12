using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public Collection<OrderedItem> OrderedItems { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Summary { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime AddedDate { get; set; }
        public ICollection<Image> Images { get; set; }

        public Product()
        {
            Id = Guid.NewGuid();
            AddedDate = DateTime.Now;
            Images = new Collection<Image>();
            ProductCategories = new Collection<ProductCategory>();
            Carts = new Collection<Cart>();
            OrderedItems = new Collection<OrderedItem>();
        }
    }
}
