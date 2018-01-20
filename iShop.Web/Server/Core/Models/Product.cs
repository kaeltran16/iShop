using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Web.Server.Commons.BaseClasses;

namespace iShop.Web.Server.Core.Models
{
    public class Product : EntityBase
    {
        public Inventory Inventory { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Summary { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime AddedDate { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public Collection<OrderedItem> OrderedItems { get; set; }
        public ICollection<Cart> Carts { get; set; }

        public Product()
        {
            AddedDate = DateTime.Now;
            Images = new Collection<Image>();
            ProductCategories = new Collection<ProductCategory>();
            Carts = new Collection<Cart>();
            OrderedItems = new Collection<OrderedItem>();
        }
    }
}
