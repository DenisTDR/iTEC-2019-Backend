using API.Base.Web.Base.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace API.StartApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((context, builder) => { builder.AddJsonFile("appsettings.json", false); })
                .UseUrls("http://0.0.0.0:" + EnvVarManager.GetOrThrow("LISTEN_PORT"));
    }
}