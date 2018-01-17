using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AspNet.Security.OpenIdConnect.Primitives;
using iShop.Web.Server.Commons.Helpers;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Persistent;
using iShop.Web.Server.Persistent.Repositories.Commons;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using iShop.Web.Server.Persistent.UnitOfWork.Commons;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace iShop.Web.Server.Commons.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IImagesRepository, ImageRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }


        public static IServiceCollection AddCustomDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Startup.Configuration.GetConnectionString("Default"));

                // Use OpenIddict
                options.UseOpenIddict<Guid>();
            });

            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
                {
                    // Custom password requirements
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.User.RequireUniqueEmail = true;
                })
                // Specify where this data will be stored
                .AddEntityFrameworkStores<ApplicationDbContext>()
                // Add token for reseting password, email..
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Set the type of claim 
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });
            return services;
        }

        public static IServiceCollection AddCustomAuthenication(this IServiceCollection services)
        {
            // Get the token settings
            var tokenSettings = new JwtTokenSettings();
            Startup.Configuration.GetSection("JwtTokenSettings").Bind(tokenSettings);
            services.AddSingleton(tokenSettings);
        
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            services.AddAuthentication(opt=>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.Audience = tokenSettings.Audience;
                    opt.Authority = tokenSettings.Authority;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.Key)),
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidAudience = tokenSettings.Audience
                    };

                });

            return services;
        }

        public static IServiceCollection AddCustomOpenIddict(this IServiceCollection services)
        {
            var tokenSettings = new JwtTokenSettings();
            Startup.Configuration.GetSection("JwtTokenSettings").Bind(tokenSettings);
            services.AddSingleton(tokenSettings); 

            services.AddOpenIddict<Guid>(options =>
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
                    .AllowAuthorizationCodeFlow()
                    .AllowRefreshTokenFlow()
                    // To enable external logins to authenticate
                    .AllowImplicitFlow();
                options.RegisterScopes(OpenIdConnectConstants.Scopes.Profile);
                options.DisableHttpsRequirement();
                options.AddSigningKey(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.Key)));
                options.SetAccessTokenLifetime(TimeSpan.FromMinutes(tokenSettings.TokenLifeTime));
                options.SetIdentityTokenLifetime(TimeSpan.FromMinutes(tokenSettings.TokenLifeTime));
                options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(tokenSettings.RefreshTokenLifeTime));
                options.UseJsonWebTokens();
                options.AddEphemeralSigningKey();
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy(ApplicationConstants.PolicyName.SuperUsers,
                    p => p.RequireClaim(ApplicationConstants.RoleName.SuperUser, "true"));

                cfg.AddPolicy(ApplicationConstants.PolicyName.Users,
                    p => p.RequireClaim(ApplicationConstants.RoleName.User, "true"));

            });
            return services;
        }
        
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(opt =>
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            return services;
        }
    }
}
