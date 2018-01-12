using System;
using System.Linq;
using AutoMapper;
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
                .ForMember(o => o.Invoice, opt => opt.Ignore())
                .ForMember(o => o.Shipping, opt => opt.Ignore())
                .AfterMap((or, o) =>
                {
                    // Get the list of added Items
                    var addedOrderedItems = or.OrderedItems
                        .Where(oir => o.OrderedItems.All(oi => oi.ProductId != oir.ProductId))
                        .Select(oir =>
                            new OrderedItem() { ProductId = oir.ProductId, Quantity = oir.Quantity, OrderId = or.Id })
                        .ToList();

                    // Add it to the database
                    foreach (var oi in addedOrderedItems)
                        o.OrderedItems.Add(oi);

                    var removedFeatures =
                        o.OrderedItems.Where(oi => or.OrderedItems.Any(oir => oir.ProductId != oi.ProductId)).ToList();

                    foreach (var oi in removedFeatures)
                        o.OrderedItems.Remove(oi);

                    // create the invoice base on the data from order
                    var invoice = new Invoice() { Id = Guid.NewGuid(), OrderId = or.Id };
                    o.Invoice = invoice;

                    // create the shipping base on the data from order
                    var shipping = new Shipping()
                    {
                        Id = Guid.NewGuid(),
                    };
                    Mapper.Map<ShippingResource, Shipping>(or.Shipping, shipping);
                    o.Shipping = shipping;

                    // assign value in the order table
                    o.InvoiceId = o.Invoice.Id;
                    o.ShippingId = o.Shipping.Id;
                });



        }
    }
}
