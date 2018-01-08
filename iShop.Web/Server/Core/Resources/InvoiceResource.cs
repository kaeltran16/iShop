using System;

namespace iShop.Web.Server.Core.Resources
{
    public class InvoiceResource
    {
        public Guid Id { get; set; }
        public OrderResource Order { get; set; }
    }
}