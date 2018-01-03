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
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }
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
        }
    }
}
