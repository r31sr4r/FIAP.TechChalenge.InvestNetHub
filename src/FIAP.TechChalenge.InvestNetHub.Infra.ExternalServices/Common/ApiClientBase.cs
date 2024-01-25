using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Common
{
    public abstract class ApiClientBase
    {
        protected HttpClient Client;
        protected string? ApiKey;
        protected string? BaseUrl;

        protected ApiClientBase(string apiKeyConfigPath, string baseUrlConfigPath)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            Client = new HttpClient();
            ApiKey = configuration[apiKeyConfigPath];
            BaseUrl = configuration[baseUrlConfigPath];
        }
    }
}
