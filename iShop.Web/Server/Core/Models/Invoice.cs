using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Invoice()
        {
            Id = Guid.NewGuid();
            InvoiceDate = DateTime.Now;
        }
    }
}
