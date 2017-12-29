using System;
using AspNet.Security.OpenIdConnect.Primitives;
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
using Microsoft.AspNetCore.Http;

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

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));

                // Use OpenIddict
                options.UseOpenIddict();
            });
            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            services.Configure<IdentityOptions>(options =>
                {
                    options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                    options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                    options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
                });


            services.AddOpenIddict(options =>
            {
                options.AddEntityFrameworkCoreStores<ApplicationDbContext>();

                options.AddMvcBinders();
                // Enable the token endpoint.
                // Form password flow (used in username/password login requests)
                options.EnableTokenEndpoint("/connect/token")

                    // Enable the authorization endpoint.
                    // Form implicit flow (used in social login redirects)
                    .EnableAuthorizationEndpoint("/connect/authorize")

                    .EnableTokenEndpoint("/connect/token")
                    .EnableUserinfoEndpoint("/api/userinfo");

                // Enable the password and the refresh token flows.
                options.AllowPasswordFlow()
                    .AllowRefreshTokenFlow()
                    // To enable external logins to authenticate
                    .AllowImplicitFlow();

                options.RequireClientIdentification();
                    
                options.SetAccessTokenLifetime(TimeSpan.FromMinutes(30));
                options.SetIdentityTokenLifetime(TimeSpan.FromMinutes(30));
                options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(60));

                options.AllowPasswordFlow();
                options.AddEphemeralSigningKey();
            });

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

            // Register the OAuth validation handler
            services.AddAuthentication()
                .AddOAuthValidation();


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
                // Instead of redirect to Home/Error, we will responce a error message
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected error occured. Try again later.");
                    });
                });
            }

            app.UseAuthentication();

            app.UseDefaultFiles();
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
