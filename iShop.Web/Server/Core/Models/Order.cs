using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class Order
    {
        [Required]
        public Guid Id { get; set; }

        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid ShippingId { get; set; }
        public Shipping Shipping { get; set; }
        public ICollection<OrderedItem> OrderedItems { get; set; }
        public DateTime OrderedDate { get; set; }

        public Order()
        {
            Id = Guid.NewGuid();
            OrderedItems = new Collection<OrderedItem>();
            OrderedDate = DateTime.Now;
        }
    }
}
