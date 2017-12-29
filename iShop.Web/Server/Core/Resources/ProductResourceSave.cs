using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class ProductResourceSave
    {
        public Guid Id { get; set; }
      
        public string Title { get; set; }
        public double Price { get; set; }
        public string Info { get; set; }
      
        public int Stock { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime AddedDate { get; set; }

        public int CategoryId { get; set; }
 
        public int ImageId { get; set; }
      

        public ProductResourceSave()
        {
            AddedDate = DateTime.Now;
        }
    }
}
