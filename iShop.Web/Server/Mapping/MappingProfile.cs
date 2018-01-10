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

            CreateMap<Order, OrderResource>()
                .ForMember(or => or.OrderedItems, opt => opt.MapFrom(p =>
                    p.OrderedItems.Select(pc => new OrderedItem(){ProductId = pc.ProductId, Quantity = pc.Quantity})));
                       
            CreateMap<Order, SavedOrderResource>();
                


            CreateMap<ApplicationUser, ApplicationUserResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap<Cart, CartResource>();
            CreateMap<Image, ImageResource>();
            CreateMap<Inventory, InventoryResource>();
            CreateMap<ShoppingCart, ShoppingCartResource>();
            CreateMap<ShoppingCart, SavedShoppingCartResource>();
            CreateMap<Product, ProductResource>()
                .ForMember(pr => pr.Categories,
                    opt => opt.MapFrom(p =>
                        p.ProductCategories.Select(pc => pc.Category)))
                .ForMember(pr => pr.Inventories, opt => opt.MapFrom(p => p.Inventories))
                .ForMember(pr => pr.Images, opt => opt.MapFrom(p => p.Images));

            CreateMap<Product, SavedProductResource>();
       

            CreateMap<OrderedItem, OrderedItemResource>();
            CreateMap<Shipping, ShippingResource>();
            CreateMap<Invoice, InvoiceResource>();

            // Resources to Entities

            CreateMap<CategoryResource, Category>()
                .ForMember(cr => cr.Id, opt => opt.Ignore())
                .ForMember(c => c.ProductCategories, opt => opt.Ignore());

            CreateMap<OrderResource, Order>()
                .ForMember(o => o.Id, opt => opt.Ignore());

            CreateMap<SavedOrderResource, Order>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.OrderedItems, opt => opt.MapFrom(c=>c.OrderedItems))
                .AfterMap((or, o) =>
                {
                    var addedOrderedItems = or.OrderedItems.Where(id => o.OrderedItems.All(oi => oi.ProductId != id.ProductId))
                        .Select(id => new OrderedItem() { ProductId = id.ProductId, OrderId = or.Id }).ToList();
                    foreach (var oi in addedOrderedItems)
                        o.OrderedItems.Add(oi);

                    var removedFeatures =
                        o.OrderedItems.Where(oi => or.OrderedItems.Any(oir=>oir.ProductId!=oi.ProductId)).ToList();
                    foreach (var oi in removedFeatures)
                        o.OrderedItems.Remove(oi);
                });



            CreateMap<ProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.ProductCategories, opt => opt.Ignore());
            CreateMap<SavedProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.ProductCategories, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    var addedCategories = pr.Categories.Where(id => p.ProductCategories.All(f => f.CategoryId != id))
                        .Select(id => new ProductCategory() {CategoryId = id, ProductId = pr.Id}).ToList();
                    foreach (var f in addedCategories)
                        p.ProductCategories.Add(f);

                    var removedFeatures =
                        p.ProductCategories.Where(c => !pr.Categories.Contains(c.CategoryId)).ToList();
                    foreach (var f in removedFeatures)
                        p.ProductCategories.Remove(f);
                });



         

            CreateMap<ApplicationUserResource, ApplicationUser>();
            CreateMap<OrderedItemResource, OrderedItem>();
            CreateMap<ShippingResource, Shipping>();
            CreateMap<ShippingResource, Shipping>();
            CreateMap<InvoiceResource, Invoice>();
            CreateMap<ShoppingCartResource, ShoppingCart>();
            CreateMap<SavedShoppingCartResource, ShoppingCart>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.Carts, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    var addedCategories = pr.Carts.Where(id => p.Carts.All(f => f.ProductId != id))
                        .Select(id => new Cart() {ProductId = id, ShoppingCartId = pr.Id}).ToList();
                    foreach (var f in addedCategories)
                        p.Carts.Add(f);

                    var removedFeatures = p.Carts.Where(c => !pr.Carts.Contains(c.ProductId)).ToList();
                    foreach (var f in removedFeatures)
                        p.Carts.Remove(f);
                });








        }
    }
}
