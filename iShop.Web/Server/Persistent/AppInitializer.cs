using iShop.Web.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
                new ApplicationRole {Name = "Admin", Description = "Full permission"},
                new ApplicationRole {Name = "User", Description = "Limited permission"}
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
                var admin = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    FirstName = "Khang",
                    LastName = "Tran",
                    Email = "admin@admin.com",
                };
                var createResult = _userManager
                    .CreateAsync(admin, "thisisaverylongandcomplexpassword1A").Result;

                var user = _userManager.FindByNameAsync("admin@admin.com").GetAwaiter().GetResult();

                var addRoleResult = _userManager
                    .AddToRoleAsync(user, "Admin")
                    .Result;

                var addClaimResult = _userManager.AddClaimAsync(user, new Claim("SuperUser", "True")).Result;
                if (createResult.Succeeded && addClaimResult.Succeeded && addClaimResult.Succeeded)
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
            // SAMPLE
            //if (!_context.Products.Any())
            //{
            //    var product = new Product()
            //    {
            //        Price = 16,
            //        Name = "TEST67",
            //        ExpiredDate = DateTime.Now
            //    };
            //    var category = new Category() { Name = "TEST34", Detail = "abcxyz" };
            //    product.ProductCategories = new List<ProductCategory>
            //    {
            //        new ProductCategory()
            //        {
            //            Product = product,
            //            Category = category
            //        }
            //    };

            //    _context.Products.Add(product);
            //    await _context.SaveChangesAsync();
            //};



        }






    }
}
