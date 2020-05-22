using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HospitalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var config = new ConfigurationBuilder().AddCommandLine(args).Build();
            //CreateHostBuilder(args, config).Build().Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //.ConfigureKestrel(options => options.ListenAnyIP(5000))
                    // .UseKestrel();
                    //.UseConfiguration(config); ;
                });
    }
}
