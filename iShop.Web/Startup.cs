using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.Logging;
using iShop.Web.Server.Commons.Extensions;
using iShop.Web.Server.Commons.Helpers;
using Swashbuckle.AspNetCore.Swagger;

namespace iShop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Use this method add your services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ImageSettings>(Configuration.GetSection("ImageSettings"));
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new Info {Title = "iShop APIs", Description = "API endpoints for iShop"}));
            services.AddDependencies();
            services.AddCustomDbContext();
            services.AddCustomIdentity();
            services.AddCustomAuthenication();
            services.AddCustomOpenIddict();
            services.AddCustomAuthorization();
            services.AddAutoMapper();
            services.AddCustomMvc();
        }

        /// <summary>
        ///  // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>     
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.AddDevMiddleware();
            app.AddCustomCsp();
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
