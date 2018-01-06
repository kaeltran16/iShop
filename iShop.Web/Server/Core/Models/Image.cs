using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Image()
        {
            Id = Guid.NewGuid();
        }
    }
}
