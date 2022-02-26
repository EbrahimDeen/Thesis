using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IAM.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                webBuilder.UseStartup<Startup>();
                //webBuilder.UseConfiguration(new ConfigurationBuilder()
                //        .SetBasePath(Directory.GetCurrentDirectory())
                //        .AddJsonFile("hosting.json", optional: true)
                //        .Build()
                //    );
                //webBuilder.UseKestrel();
               // webBuilder.UseUrls("https://0.0.0.0:5001", "http://0.0.0.0:5000");
        //webBuilder.UseKestrel(opts =>
        //{
        //    opts.Listen(IPAddress.Loopback, port: 8501);
        //    opts.ListenAnyIP(8502, opts => opts.UseHttps());
        //    //opts.ListenLocalhost(8503);
        //    opts.ListenLocalhost(8504, opts => opts.UseHttps());
        //});
    });
    }
}
