using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using iShop.Web.Server.Persistent.Repositories.Commons;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using iShop.Web.Server.Persistent.UnitOfWork.Commons;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;

namespace iShop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //declare interfaces
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            //services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();



            // Add the custome Identity for specify User and Role
            services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
                {
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireLowercase = true;

                    opt.User.RequireUniqueEmail = true;
                })
                // Specify where this data will be stored
                .AddEntityFrameworkStores<ApplicationDbContext>()
                // Add token for reseting password, email..
                .AddDefaultTokenProviders();

          
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));
           
            services.AddAutoMapper();
            services.AddMvc();
       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
//            app.UseWelcomePage();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
