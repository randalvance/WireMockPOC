using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WireMockPOC.Core;

namespace WireMockPOCClient
{
    public class Program
    {
        public static IConfigurationRoot Configuration = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .AddJsonFile("config.local.json", true)
            .AddEnvironmentVariables()
            .Build();

        public static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .ConfigureServices(Configuration.GetSection("App"))
                .AddOptions()
                .AddServices()
                .BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IService>();
            Console.WriteLine("Executing something...");
            await service.DoSomething();                        
        }
    }
}
