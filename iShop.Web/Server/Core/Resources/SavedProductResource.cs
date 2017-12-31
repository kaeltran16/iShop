using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class SavedProductResource
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public double Price { get; set; }
        //public string Info { get; set; }
        [Required]
        public int Stock { get; set; }
        //public DateTime ExpiredDate { get; set; }
        public DateTime AddedDate { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid ImageId { get; set; }
      

        public SavedProductResource()
        {
            AddedDate = DateTime.Now;
        }
    }
}
