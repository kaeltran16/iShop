using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class CategoryResource
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Detail { get; set; }

     }
}
