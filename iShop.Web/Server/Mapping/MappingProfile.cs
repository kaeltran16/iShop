using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;

namespace iShop.Web.Server.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {

            // forward 
            CreateMap<Product, ProductResource>()
                .ForMember(vr => vr.Category, opt => opt.MapFrom(v => v.Category));
            CreateMap<ShoppingCart, ShoppingCartResource>();
            CreateMap<Cart, CartResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap<Order, OrderResource>();
            CreateMap<Order, OrderResourceSave>();
            CreateMap<Product, ProductResourceSave>();
            CreateMap<ShoppingCart, ShoppingCartResourceSave>()
                .ForMember(vr => vr.Carts, opt => opt.MapFrom(v => v.Carts.Select(Carts => new CartResourceSave { Id = Carts.Id, Quantity = Carts.Quantity, ProductId = Carts.ProductId })));




            //backward
            CreateMap<ProductResourceSave, Product>()
                .ForMember(v => v.Id, opt => opt.Ignore());
            CreateMap<ShoppingCartResourceSave, ShoppingCart>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.Carts, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    // Remove unselected Carts
                    var removedCarts = v.Carts.Where(f => !vr.Carts.Contains(new CartResourceSave { Id = f.Id, Quantity = f.Quantity, ProductId = f.ProductId })).ToList();
                    foreach (var f in removedCarts)
                        v.Carts.Remove(f);

                    // Add new Carts
                    var addedFeatures = vr.Carts.Where(cart => !v.Carts.Any(f => f.Id == cart.Id)).Select(Carts => new Cart { Id = Carts.Id, Quantity = Carts.Quantity, ProductId = Carts.ProductId }).ToList();
                    foreach (var f in addedFeatures)
                        v.Carts.Add(f);
                });
            CreateMap<OrderResource,Order >();
            CreateMap<OrderResourceSave, Order>();
            CreateMap<ShoppingCartResource,ShoppingCart>();
            CreateMap<ProductResource, Product>();
        }
    }
}
