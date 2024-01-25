using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.ExternalServices.Common;

public class ApiClientBaseFixture
{
    public HttpClient Client { get; private set; }
    public string? ApiKey { get; private set; }

    public ApiClientBaseFixture()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("apiSettings.json")
            .Build();

        Client = new HttpClient();
        ApiKey = configuration["ExternalServices:AlphaVantage:ApiKey"];
    }
}
