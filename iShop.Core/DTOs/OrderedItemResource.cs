using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Core.DTOs
{
    public class OrderedItemResource
    {
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}