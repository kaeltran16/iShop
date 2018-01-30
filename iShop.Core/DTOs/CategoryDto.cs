using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Core.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Detail { get; set; }
        [StringLength(50)]
        public string Short { get; set; }
    }
}
