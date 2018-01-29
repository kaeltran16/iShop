using iShop.Web.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using iShop.Web.Server.Commons.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace iShop.Web.Server.Persistent
{
    // This class will responsible for seeding data in the database
    public class AppInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly Logger _logger;

        public AppInitializer(IServiceProvider services, ApplicationDbContext context, Logger logger)
        {
            _roleManager = services.GetService<RoleManager<ApplicationRole>>();
            _userManager = services.GetService<UserManager<ApplicationUser>>();
            _context = context;
            _logger = logger;
        }

        private async Task SeedRoles()
        {
            var roles = new List<ApplicationRole>()
            {
                new ApplicationRole {Name = ApplicationConstants.RoleName.SuperUser, Description = "Full permission"},
                new ApplicationRole {Name = ApplicationConstants.RoleName.User, Description = "Limited permission"}
            };

            foreach (var role in roles)
            {
                if (!_roleManager.RoleExistsAsync(role.Name).Result)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }

        private void SeedSuperUser()
        {
            if (!_context.Users.Any())
            {
                var adminInfo = Startup.Configuration.GetSection("SuperUserInfo");
                var admin = new ApplicationUser
                {
                    UserName = adminInfo.GetSection("UserName").Value,
                    FirstName = "Khang",
                    LastName = "Tran",
                    Email = adminInfo.GetSection("Email").Value,
                };


                var createResult = _userManager
                    .CreateAsync(admin, adminInfo.GetSection("Password").Value).Result;

                var user = _userManager.FindByNameAsync(adminInfo.GetSection("UserName").Value).GetAwaiter()
                    .GetResult();

                var addRoleResult = _userManager
                    .AddToRoleAsync(user, ApplicationConstants.RoleName.SuperUser)
                    .Result;
                var addClaimResult =
                    _userManager.AddClaimAsync(user, new Claim(ApplicationConstants.ClaimName.SuperUser, "true")).Result;


                if (createResult.Succeeded && addRoleResult.Succeeded && addClaimResult.Succeeded)
                    _logger.Info("Super User is created successfully");
                else
                    _logger.Error("createResult: " + createResult.ToString() + " | roleResult: " + addRoleResult +
                                  " | claimResult: " + addClaimResult);

            }
        }

        public async Task Seed()
        {
            await SeedRoles();
            SeedSuperUser();
            //SAMPLE
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


                if (!_context.Images.Any())
                {
                    var images = new List<Image>()
                    {
                        new Image()
                        {
                            FileName = "ca-duoi.jpg",
                            ProductId = _context.Products.FirstOrDefault(x => x.Name == "Cá Đuối Đại Dương").Id
                        },
                        new Image()
                        {
                            FileName = "ca-dieu-hong.jpg",
                            ProductId = _context.Products.FirstOrDefault(x => x.Name == "Cá Diêu Hồng").Id
                        },
                        new Image()
                        {
                            FileName = "thit-ga.jpg",
                            ProductId = _context.Products.FirstOrDefault(x => x.Name == "Thịt Gà Ta").Id
                        },
                        new Image()
                        {
                            FileName = "thit-heo.jpg",
                            ProductId = _context.Products.FirstOrDefault(x => x.Name == "Thịt Heo Nam Định").Id
                        },
                        new Image()
                        {
                            FileName = "thit-bo.jpg",
                            ProductId = _context.Products.FirstOrDefault(x => x.Name == "Thịt Bò Hải Dương").Id
                        },

                    };
                    await _context.AddRangeAsync(images);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Categories.Any())
                {
                    var categories = new List<Category>()
                    {
                        new Category() {Name = "Thịt Heo", Detail = "abcxyz"},
                        new Category() {Name = "Thịt Bò", Detail = "abcxyz"},
                        new Category() {Name = "Thịt Gà và Trứng", Detail = "abcxyz"},
                        new Category() {Name = "Thủy Sản", Detail = "abcxyz"},
                        new Category() {Name = "Hải Sản", Detail = "abcxyz"}
                    };
                    await _context.AddRangeAsync(categories);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Suppliers.Any())
                {
                    var suppliers = new List<Supplier>()
                    {
                     new Supplier(){Name = "Hai long",Description = "La  một nhà cung cấp tốt"},
                        new Supplier(){Name = "Hai long2",Description = "La  một nhà cung cấp tốt1"},
                        new Supplier(){Name = "Hai long3",Description = "La  một nhà cung cấp tốt2"},
                        new Supplier(){Name = "Hai long4",Description = "La  một nhà cung cấp tốt3"},
                        new Supplier(){Name = "Hai long5",Description = "La  một nhà cung cấp tốt4"},
                    };
                    await _context.AddRangeAsync(suppliers);
                    await _context.SaveChangesAsync();
                }

                // SAMPLE
                if (!_context.Products.Any())
                {

                    var product1 = new Product()
                    {
                        Price = 16,
                        Name = "Thịt Heo Nam Định",
                        ExpiredDate = DateTime.Now,
                       
                    };

                    product1.ProductCategories = new List<ProductCategory>
                    {
                        new ProductCategory()
                        {
                            Product = product1,
                            CategoryId = _context.Categories.FirstOrDefault(x => x.Name == "Thịt Heo").Id
                        }
                    };

                    product1.Inventory=new Inventory()
                    {
                        Stock = 10,
                        SupplierId = _context.Suppliers.FirstOrDefault(i=>i.Name== "Hai long").Id
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
                    product2.Inventory = new Inventory()
                    {
                        Stock = 100,
                        SupplierId = _context.Suppliers.FirstOrDefault(i => i.Name == "Hai long2").Id
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
                    product3.Inventory = new Inventory()
                    {
                        Stock = 101,
                        SupplierId = _context.Suppliers.FirstOrDefault(i => i.Name == "Hai long3").Id
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
                        },
                      
                    };
                    product4.Inventory = new Inventory()
                    {
                        Stock = 104,
                        SupplierId = _context.Suppliers.FirstOrDefault(i => i.Name == "Hai long4").Id
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
                    product5.Inventory = new Inventory()
                    {
                        Stock = 1067,
                        SupplierId = _context.Suppliers.FirstOrDefault(i => i.Name == "Hai long5").Id
                    };
                    var products = new List<Product>()
                    {
                        product1,
                        product2,
                        product3,
                        product5,
                        product4

                    };

                    await _context.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                };


            }

                ;

        }






    }
}
