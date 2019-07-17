using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration; 

namespace Research.Web.Host.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json").Build();
            var config = new ConfigurationBuilder()
               .AddCommandLine(args)
               .Build();
            string ip = config["ip"];
            string port = config["port"];

            return WebHost.CreateDefaultBuilder(args).UseConfiguration(configuration)
                .UseStartup<Startup>()
                //.UseUrls($"http://{ip}:{port}")
                .Build(); 
        }


    }
}
