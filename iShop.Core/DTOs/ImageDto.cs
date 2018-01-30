using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Core.DTOs
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
    }
}
