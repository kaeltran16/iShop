using iShop.Web.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Persistent
{
    // This class will responsible for seeding data in the database
    public class AppInitializer
    {
        private readonly ApplicationDbContext _context;

        public AppInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {


            if (!_context.Categories.Any())
            {
                var categories = new List<Category>()
                {
                    new Category(){ Name = "Thịt Heo", Detail = "abcxyz" },
                    new Category(){ Name = "Thịt Bò", Detail = "abcxyz" },
                    new Category(){ Name = "Thịt Gà và Trứng", Detail = "abcxyz" },
                    new Category(){ Name = "Thủy Sản", Detail = "abcxyz" },
                    new Category(){ Name = "Hải Sản", Detail = "abcxyz" }
                };
                await _context.AddRangeAsync(categories);
                await _context.SaveChangesAsync();
            }

                // SAMPLE
                if (!_context.Products.Any())
            {
               
                var product1 = new Product()
                {
                    Price = 16,
                    Name = "Thịt Heo Nam Định",
                    ExpiredDate = DateTime.Now
                };
              
                product1.ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory()
                    {
                        Product = product1,
                        CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Thịt Heo").Id
                    }
                };
                var product2 = new Product()
                {
                    Price = 16,
                    Name = "Thịt Bò Hải Dương",
                    ExpiredDate = DateTime.Now
                };
                
                product2.ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory()
                    {
                        Product = product2,
                        CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Thịt Bò").Id
                    }
                };
                var product3 = new Product()
                {
                    Price = 16,
                    Name = "Thịt Gà Ta",
                    ExpiredDate = DateTime.Now
                };

                product3.ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory()
                    {
                        Product = product3,
                        CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Thịt Gà và Trứng").Id
                    }
                };
                var product4 = new Product()
                {
                    Price = 16,
                    Name = "Cá Diêu Hồng",
                    ExpiredDate = DateTime.Now
                };

                product4.ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory()
                    {
                        Product = product4,
                        CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Thủy Sản").Id
                    }
                };

                var product5 = new Product()
                {
                    Price = 16,
                    Name = "Cá Đuối Đại Dương",
                    ExpiredDate = DateTime.Now
                };

                product5.ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory()
                    {
                        Product = product5,
                        CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Hải Sản").Id
                    }
                };

                var products = new List<Product>()
                {
                    product1,
                    product2,
                    product3,
                    product5,
                    product4,

                };

                await _context.AddRangeAsync(products);
                await _context.SaveChangesAsync();
            };


            if (!_context.Images.Any())
            {
                var images = new List<Image>()
                {
                    new Image(){ FileName = "ca-duoi.jpg",
                        ProductId = _context.Products.FirstOrDefault(x => x.Name == "Cá Đuối Đại Dương").Id },
                    new Image(){ FileName = "ca-dieu-hong.jpg",
                        ProductId = _context.Products.FirstOrDefault(x => x.Name == "Cá Diêu Hồng").Id },
                    new Image(){ FileName = "thit-ga.jpg",
                        ProductId = _context.Products.FirstOrDefault(x => x.Name == "Thịt Gà Ta").Id },
                    new Image(){ FileName = "thit-heo.jpg",
                        ProductId = _context.Products.FirstOrDefault(x => x.Name == "Thịt Heo Nam Định").Id },
                    new Image(){ FileName = "thit-bo.jpg",
                        ProductId = _context.Products.FirstOrDefault(x => x.Name == "Thịt Bò Hải Dương").Id },
                    
                };
                await _context.AddRangeAsync(images);
                await _context.SaveChangesAsync();
            }

        }

         




        }
}
