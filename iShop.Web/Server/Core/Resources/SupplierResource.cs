using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iShop.Web.Server.Core.Resources
{
    public class SupplierResource
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
    }
}