using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Core.DTOs
{
    public class InventoryDto
    {
        public Guid ProductId { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
