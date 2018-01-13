using System;
using iShop.Web.Server.Commons.BaseClasses;

namespace iShop.Web.Server.Core.Models
{
    public class Invoice : EntityBase
    {
        public DateTime InvoiceDate { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Invoice()
        {
            InvoiceDate = DateTime.Now;
        }
    }
}
