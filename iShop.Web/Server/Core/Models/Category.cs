using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class Category
    {      
        public Guid Id { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Short { get; set; }

        public Category()
        {
            Id = Guid.NewGuid();
            ProductCategories = new Collection<ProductCategory>();
        }
    }
}
