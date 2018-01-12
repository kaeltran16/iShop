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

            // SAMPLE
            if (!_context.Products.Any())
            {
                var product = new Product()
                {
                    Price = 16,
                    Name = "TEST67",
                    ExpiredDate = DateTime.Now
                };
                var category = new Category() { Name = "TEST34", Detail = "abcxyz" };
                product.ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory()
                    {
                        Product = product,
                        Category = category
                    }
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            };

           
             
            }

         




        }
}
