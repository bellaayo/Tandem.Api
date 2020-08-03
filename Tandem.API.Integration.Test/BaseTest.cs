using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace Tandem.API.Integration.Test
{
    public class BaseTest
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }

        public BaseTest()
        {
            SetUpClient();
        }

        private void SetUpClient()
        {

            _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>()
            .ConfigureAppConfiguration((context, conf) =>
            {
                conf.AddJsonFile("appsettings.json");
            }));

            Client = _server.CreateClient();
        }
    }
}
