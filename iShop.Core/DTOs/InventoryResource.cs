using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Core.DTOs
{
    public class InventoryResource
    {
        public Guid ProductId { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
