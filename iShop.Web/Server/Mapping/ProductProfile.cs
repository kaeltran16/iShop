using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Web.Server.Commons.BaseClasses;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;

namespace iShop.Web.Server.Mapping
{
    public class ProductProfile: BaseProfile
    {
   

        protected override void CreateMap()
        {

            CreateMap<Product, SavedProductResource>();

            CreateMap<Product, ProductResource>()
                .ForMember(pr => pr.Categories,
                    opt => opt.MapFrom(p =>
                        p.ProductCategories.Select(pc => pc.Category)))
                .ForMember(pr => pr.Inventory, opt => opt.MapFrom(p => p.Inventory))
                .ForMember(pr => pr.Images, opt => opt.MapFrom(p => p.Images))
                .ForMember(pr => pr.SupplierId, opt => opt.MapFrom(p => p.Inventory.SupplierId));


            CreateMap<ProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.ProductCategories, opt => opt.Ignore());


            CreateMap<SavedProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.ProductCategories, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    var addedCategories = pr.Categories.Where(id => p.ProductCategories.All(pc => pc.CategoryId != id))
                        .Select(id => new ProductCategory() { CategoryId = id, ProductId = pr.Id }).ToList();
                    foreach (var pc in addedCategories)
                        p.ProductCategories.Add(pc);

                    var removedCategories =
                        p.ProductCategories.Where(c => !pr.Categories.Contains(c.CategoryId)).ToList();
                    foreach (var pc in removedCategories)
                        p.ProductCategories.Remove(pc);
                })

                .ForMember(p => p.Inventory, opt => opt.Ignore())
                .AfterMap((pr, p) =>
                {
                    var inventory = new Inventory() { ProductId = pr.Id, SupplierId = pr.SupplierId, Stock = pr.Stock };
                    p.Inventory = inventory;
                });

        }
    }
}
