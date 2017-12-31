using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class SavedOrderResource
    {
        [Required]
        public Guid UserId { get; set; }
      
        [Required]
        public int ShoppingCartId { get; set; }
      
        public DateTime PlacedDate { get; set; }

        public SavedOrderResource()
        {
            PlacedDate = DateTime.Now;
        }
    }
}
