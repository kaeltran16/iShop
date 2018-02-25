using System;
using iShop.Web.Server.Persistent;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;

namespace iShop.Web
{
    public class Program
    {      
        public static void Main(string[] args)
        {
            // Setting NLog, see nlog.config for the output configurations
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("Init Main Program");
                var host = BuildWebHost(args);
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        // Set the databas
                        var context = services.GetRequiredService<ApplicationDbContext>();
                        AppInitializer initializer = new AppInitializer(services, context, logger);
                        initializer.Seed().Wait();
                    }
                    catch (Exception ex)
                    {                   
                        logger.Error(ex, "An error occurred while seeding the database.");
                    }
                    host.Run();
                }
            }
            catch (Exception e)
            {
                //NLog: catch setup errors
                logger.Error(e, "Stopped program because of exception");
                throw;
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
        //Startup.Configuration.GetSection("Debug").GetSection("Url").ToString()
                .UseUrls("http://localhost:58890")
                // UseNLog
                .UseNLog()
                .Build();
    }
}
