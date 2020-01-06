using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ragnarok.GxG.Extensions;
using Serilog;
using System;
using System.IO;

namespace ChipEmailer
{
    class Startup
    {
        static public IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                Configuration = builder.Build();

                var services = new ServiceCollection();
                ConfigureServices(services);

                var serviceProvider = services.BuildServiceProvider();

                // entry to run app
                serviceProvider.GetService<ChipEmailer>().Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                string msg = e.Message + "\r\n" + e.StackTrace + "\r\n";
                if (e.InnerException != null)
                {
                    msg += e.InnerException.Message + "\r\n" + e.InnerException.StackTrace;
                }
                File.AppendAllText(_createLogFileName(suffix: "exception_"), msg);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Configuration
            services.AddSingleton<IConfiguration>(Configuration);

            // Security
            services.AddDataProtection();

            // Add Ragnarok services
            services.AddRagnarokServices(Configuration);

            // Add logging provider
            services.AddLogging().AddSingleton(new LoggerFactory()
                .AddSerilog(
                    new LoggerConfiguration()
                      .Enrich.FromLogContext()
                      .WriteTo.File(_createLogFileName())
                      .CreateLogger()
                ).CreateLogger<ChipEmailer>()
            );

            // This app
            services.AddTransient<ChipEmailer>();
        }

        private static string _createLogFileName(string suffix = "")
        {
            return Configuration["Logging:OutDir"] + "\\ChipEmailer_" + suffix + DateTime.Now.ToString("yyyMMdd_HHmmss") + ".log";
        }
    }
}