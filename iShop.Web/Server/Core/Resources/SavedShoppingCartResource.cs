using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class SavedShoppingCartResource
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Guid> Carts { get; set; }
        [Required]
        public DateTime PlacedDate { get; set; }
    }
}
