using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(155)]
        public string Title { get; set; }
        public double Price { get; set; }
        public string Info { get; set; }
        [Required]
        public int Stock { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime AddedDate { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public int ImageId { get; set; }
        public Image Image { get; set; }

        public Product()
        {
            AddedDate = DateTime.Now;
        }
    }
}
