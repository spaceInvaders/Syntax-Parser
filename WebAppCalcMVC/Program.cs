using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAppCalcMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(ConfigLogging)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        // ConfigureLogging method allows to be called after logging is configured to add our configuration
        // adding a filter for the category of log and make it appear on our log level
        static void ConfigLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
        }
    }
}
