using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;

namespace iShop.Web.Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entities to Resources

            CreateMap<Category, CategoryResource>();
            CreateMap<Supplier, SupplierResource>();

            CreateMap<Order, OrderResource>()
                .ForMember(or => or.OrderedItems, opt => opt.MapFrom(p =>
                    p.OrderedItems.Select(pc => new OrderedItem() {ProductId = pc.ProductId, Quantity = pc.Quantity})))
                .ForMember(or => or.Shipping, opt => opt.MapFrom(o => o.Shipping))
                .ForMember(or => or.Invoice, opt => opt.MapFrom(o => o.Invoice));
                
            CreateMap<Order, SavedOrderResource>();

            CreateMap<ShoppingCart, ShoppingCartResource>()
                .ForMember(or => or.Carts, opt => opt.MapFrom(p =>
                    p.Carts.Select(pc => new Cart() { ProductId = pc.ProductId, Quantity = pc.Quantity })));

            CreateMap<ShoppingCart, SavedShoppingCartResource>();
            CreateMap<Product, SavedProductResource>();

            CreateMap<Product, ProductResource>()
                .ForMember(pr => pr.Categories,
                    opt => opt.MapFrom(p =>
                        p.ProductCategories.Select(pc => pc.Category)))
                .ForMember(pr => pr.Inventory, opt => opt.MapFrom(p => p.Inventory))
                .ForMember(pr => pr.Images, opt => opt.MapFrom(p => p.Images))
                .ForMember(pr => pr.SupplierId, opt => opt.MapFrom(p => p.Inventory.SupplierId));

            CreateMap<Image, ImageResource>();
            CreateMap<ApplicationUser, ApplicationUserResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap<Cart, CartResource>();
            CreateMap<Image, ImageResource>();
            CreateMap<Inventory, InventoryResource>();
        
            

       

            CreateMap<OrderedItem, OrderedItemResource>();
            CreateMap<Shipping, ShippingResource>();
            CreateMap<Invoice, InvoiceResource>();

            // Resources to Entities

            CreateMap<CategoryResource, Category>()
                .ForMember(cr => cr.Id, opt => opt.Ignore())
                .ForMember(c => c.ProductCategories, opt => opt.Ignore());

            CreateMap<SupplierResource, Supplier>()
                .ForMember(cr => cr.Id, opt => opt.Ignore())
                .ForMember(c => c.Inventories, opt => opt.Ignore());

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
                            new OrderedItem() {ProductId = oir.ProductId, Quantity = oir.Quantity, OrderId = or.Id})
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
                    var invoice = new Invoice() {Id = Guid.NewGuid(), OrderId = or.Id};
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





            CreateMap<SavedProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.ProductCategories, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    var addedCategories = pr.Categories.Where(id => p.ProductCategories.All(pc => pc.CategoryId != id))
                        .Select(id => new ProductCategory() {CategoryId = id, ProductId = pr.Id}).ToList();
                    foreach (var pc in addedCategories)
                        p.ProductCategories.Add(pc);

                    var removedFeatures =
                        p.ProductCategories.Where(c => !pr.Categories.Contains(c.CategoryId)).ToList();
                    foreach (var pc in removedFeatures)
                        p.ProductCategories.Remove(pc);
                })
                
                .ForMember(p => p.Inventory, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    var inventory = new Inventory() {ProductId = pr.Id, SupplierId = pr.SupplierId, Stock = pr.Stock};
                    p.Inventory = inventory;
                });
                



            CreateMap<ShoppingCartResource, ShoppingCart>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.Carts, opt => opt.Ignore());

            CreateMap<SavedShoppingCartResource, ShoppingCart>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.Carts, opt => opt.MapFrom(c => c.Carts))
                .AfterMap((sr, s) =>
                {
                    var addedCarts = sr.Carts.Where(cr => s.Carts.All(c => c.ProductId != cr.ProductId))
                        .Select(cr => new Cart() { ProductId = cr.ProductId, Quantity = cr.Quantity, ShoppingCartId = sr.Id}).ToList();
                    foreach (var c in addedCarts)
                        s.Carts.Add(c);

                    var removedCarts =
                        s.Carts.Where(c => sr.Carts.Any(cr => cr.ProductId != c.ProductId)).ToList();
                    foreach (var c in removedCarts)
                        s.Carts.Remove(c);
                });

            CreateMap<ProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.ProductCategories, opt => opt.Ignore());
            CreateMap<ImageResource, Image>();
            CreateMap<CartResource, Cart>();

            CreateMap<ApplicationUserResource, ApplicationUser>();
            CreateMap<OrderedItemResource, OrderedItem>();
            CreateMap<ShippingResource, Shipping>()
                .ForMember(sr => sr.Id, opt => opt.Ignore());
  
            CreateMap<InvoiceResource, Invoice>()
                .ForMember(sr => sr.Id, opt => opt.Ignore());

            CreateMap<InventoryResource, Inventory>();







        }
    }
}
