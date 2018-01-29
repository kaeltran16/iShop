using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iShop.Core.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Short { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

        public Category()
        {
            ProductCategories = new Collection<ProductCategory>();
        }
    }
}
