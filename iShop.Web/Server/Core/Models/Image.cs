using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [StringLength(155)]
        public string FileName { get; set; }
     
      
    }
}
