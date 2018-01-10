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

            CreateMap<ApplicationUser, ApplicationUserResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap<Cart, CartResource>();
            CreateMap<Image, ImageResource>();
            CreateMap<Inventory, InventoryResource>();
            CreateMap<ShoppingCart, ShoppingCartResource>();

            CreateMap<Product, ProductResource>()
            .ForMember(pr => pr.Categories,
                opt => opt.MapFrom(p =>
                    p.ProductCategories.Select(pc => pc.Category)))
            .ForMember(pr => pr.Inventories, opt => opt.MapFrom(p => p.Inventories))
            .ForMember(pr => pr.Images, opt => opt.MapFrom(p => p.Images));

            CreateMap<Order, OrderResource>();

            CreateMap<Order, SavedOrderResource>();

            CreateMap<OrderedItem, OrderedItemResource>();
            CreateMap<Shipping, ShippingResource>();
            CreateMap<Invoice, InvoiceResource>();

            // Resources to Entities

            CreateMap<CategoryResource, Category>()
                .ForMember(cr => cr.Id, opt => opt.Ignore())
                .ForMember(c => c.ProductCategories, opt => opt.Ignore());

            CreateMap<ProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.ProductCategories, opt => opt.Ignore());

            CreateMap<OrderResource, Order>();
            CreateMap<SavedOrderResource, Order>()
                .ForMember(o => o.Id, opt => opt.Ignore());

            CreateMap<ApplicationUserResource, ApplicationUser>();
            CreateMap<OrderedItemResource, OrderedItem>();
            CreateMap<ShippingResource, Shipping>();
            CreateMap<ShippingResource, Shipping>();
            CreateMap<InvoiceResource, Invoice>();
            //    CreateMap<CategoryResource, Category>();



            //    CreateMap<ProductResource, Product>()
            //    .ForMember(c => c.ProductCategories, opt => opt.Ignore())
            //.AfterMap((pr, p) =>
            //    {
            //        var addedFeatures = pr.Categories.Where(id => p.ProductCategories.All(f => f.CategoryId != id.Id))
            //            .Select(id => new ProductCategory() {CategoryId = id.Id, ProductId = pr.Id}).ToList();
            //    foreach (var f in addedFeatures)
            //        p.ProductCategories.Add(f);
            //});

            //CreateMap<CategoryResource, Category>()
            //    .ForMember(c => c.ProductCategories, opt => opt.Ignore())
            //    .AfterMap((cr, c) =>
            //    {
            //        foreach (var prodCate in c.ProductCategories)
            //        {
            //            foreach (var prod in cr.Products)
            //            {
            //                prodCate.ProductId = prod.Id;
            //                prodCate.CategoryId = x
            //            }
            //        }
            //    });



            //            //CreateMap<Product, ProductResource>()
            //            //    .ForMember(vr => vr.Category, opt => opt.MapFrom(v => v.Category));
            //            //CreateMap<ShoppingCart, ShoppingCartResource>();
            //            //CreateMap<Cart, CartResource>();
            //            //CreateMap<Category, CategoryResource>();
            //            //CreateMap<Order, OrderResource>();
            //            //CreateMap<Order, SavedOrderResource>();
            //            //CreateMap<Product, SavedProductResource>();
            //            //CreateMap<ShoppingCart, ShoppingCartResourceSave>()
            //            //    .ForMember(vr => vr.Carts, opt => opt.MapFrom(v => v.Carts.Select(Carts => new CartResourceSave { Id = Carts.Id, Quantity = Carts.Quantity, ProductId = Carts.ProductId })));




            //            //backward
            //            CreateMap<SavedProductResource, Product>()
            //                .ForMember(v => v.Id, opt => opt.Ignore());
            //            CreateMap<ShoppingCartResourceSave, ShoppingCart>()
            //                .ForMember(v => v.Id, opt => opt.Ignore())
            //                .ForMember(v => v.Carts, opt => opt.Ignore())
            //                .AfterMap((vr, v) =>
            //                {
            //                    // Remove unselected Carts
            //                    var removedCarts = v.Carts.Where(f => !vr.Carts.Contains(new CartResourceSave { Id = f.Id, Quantity = f.Quantity, ProductId = f.ProductId })).ToList();
            //                    foreach (var f in removedCarts)
            //                        v.Carts.Remove(f);

            //                    // Add new Carts
            //                    var addedFeatures = vr.Carts.Where(cart => !v.Carts.Any(f => f.Id == cart.Id)).Select(Carts => new Cart { Id = Carts.Id, Quantity = Carts.Quantity, ProductId = Carts.ProductId }).ToList();
            //                    foreach (var f in addedFeatures)
            //                        v.Carts.Add(f);
            //                });
            //            CreateMap<OrderResource, Order>();
            //            CreateMap<SavedOrderResource, Order>();
            //            CreateMap<ShoppingCartResource, ShoppingCart>();
            //            CreateMap<ProductResource, Product>()
            //                .ForMember(v => v.Id, opt => opt.Ignore());
            //            CreateMap<CategoryResource, Category>()
            //                .ForMember(v => v.Id, opt => opt.Ignore());
        }
    }
}
