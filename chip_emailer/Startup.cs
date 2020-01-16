using ChipEmailer.Contexts;
using ChipEmailer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        Console.WriteLine("GERALD");
                var env = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
                Console.WriteLine(env);
                if (string.IsNullOrEmpty(env))
                {
                  env = "production";
                }

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: true);
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

            // Add logging provider
            services.AddLogging().AddSingleton(new LoggerFactory()
                .AddSerilog(
                    new LoggerConfiguration()
                      .Enrich.FromLogContext()
                      .WriteTo.File(_createLogFileName())
                      .CreateLogger()
                ).CreateLogger<ChipEmailer>()
            );

      services.AddScoped<IChipEmailerDbContext>(provider => new ChipEmailerDbContext(Configuration["DbSettings:Finch:ConnectionString"]));

      services.AddScoped<ICacheRepository, CacheRepository>();
      services.AddScoped<IFinchRepository, FinchRepository>();

      // This app
      services.AddTransient<ChipEmailer>();
        }

        private static string _createLogFileName(string suffix = "")
        {
            return Configuration["Logging:OutDir"] + "\\ChipEmailer_" + suffix + DateTime.Now.ToString("yyyMMdd_HHmmss") + ".log";
        }
    }
}