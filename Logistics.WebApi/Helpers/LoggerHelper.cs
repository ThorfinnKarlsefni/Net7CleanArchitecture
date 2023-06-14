using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Logistics.WebApi.Helpers
{
    public class LoggerHelper
    {
        public static void ConfigureService(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });
        }
    }
}

