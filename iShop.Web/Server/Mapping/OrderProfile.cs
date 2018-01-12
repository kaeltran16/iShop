using System;
using System.Linq;
using iShop.Web.Server.Commons.BaseClasses;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;

namespace iShop.Web.Server.Mapping
{
    public class OrderProfile : BaseProfile
    {
 

        protected override void CreateMap()
        {
            CreateMap<Order, SavedOrderResource>();

            CreateMap<Order, OrderResource>()
                .ForMember(or => or.OrderedItems, opt => opt.MapFrom(p =>
                    p.OrderedItems.Select(pc => new OrderedItem() { ProductId = pc.ProductId, Quantity = pc.Quantity })))
                .ForMember(or => or.Shipping, opt => opt.MapFrom(o => o.Shipping))
                .ForMember(or => or.Invoice, opt => opt.MapFrom(o => o.Invoice));

            CreateMap<OrderResource, Order>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(d => d.OrderedItems, opt => opt.Ignore());

            CreateMap<SavedOrderResource, Order>()
           .ForMember(o => o.Id, opt => opt.Ignore())
           .ForMember(o => o.OrderedItems, opt => opt.Ignore())
           .AfterMap((or, o) =>
           {
               var addedOrderedItems = or.OrderedItems
                   .Where(oir => o.OrderedItems.All(oi => oi.ProductId != oir.ProductId))
                   .Select(oir =>
                       new OrderedItem() { ProductId = oir.ProductId, Quantity = oir.Quantity, OrderId = or.Id })
                   .ToList();
               foreach (var oi in addedOrderedItems)
                   o.OrderedItems.Add(oi);

               var removedFeatures =
                   o.OrderedItems.Where(oi => or.OrderedItems.Any(oir => oir.ProductId != oi.ProductId)).ToList();
               foreach (var oi in removedFeatures)
                   o.OrderedItems.Remove(oi);
           })
           .ForMember(o => o.Invoice, opt => opt.Ignore())
           .AfterMap((or, o) =>
           {
               var invoice = new Invoice() { Id = Guid.NewGuid(), OrderId = or.Id };
               o.Invoice = invoice;
           })
           .ForMember(o => o.Shipping, opt => opt.Ignore())
           .AfterMap((or, o) =>
           {
               var shipping = new Shipping()
               {
                   Id = Guid.NewGuid(),
                   OrderId = or.Id,
                   Charge = or.Shipping.Charge,
                   City = or.Shipping.City,
                   ShippingDate = or.Shipping.ShippingDate,
                   ShippingState = or.Shipping.ShippingState,
                   Street = or.Shipping.Street
               };
               o.Shipping = shipping;
           })
           .AfterMap((or, o) =>
           {
               o.InvoiceId = o.Invoice.Id;
               o.ShippingId = o.Shipping.Id;
           });

        }
    }
}
