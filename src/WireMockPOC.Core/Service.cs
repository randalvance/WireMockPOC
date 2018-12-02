using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WireMockPOC.Core.Configurations;

namespace WireMockPOC.Core
{
    public class Service : IService
    {
        private readonly IOptions<ApplicationSettings> _applicationSettings;

        public Service(IOptions<ApplicationSettings> applicationSettings)
        {
            _applicationSettings = applicationSettings;    
        }

        public async Task DoSomething()
        {
            var baseUrl = _applicationSettings.Value.ApiEndpoint;

            var httpClient = new HttpClient();
            Console.WriteLine("Base URL within application: " + baseUrl);
            var result = await httpClient.GetAsync($"{baseUrl}/test");

            result.EnsureSuccessStatusCode();
            var json = await result.Content.ReadAsStringAsync();
            Console.WriteLine("JSON is " + json);
        }
    }
}
