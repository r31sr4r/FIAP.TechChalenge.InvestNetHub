using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.ExternalServices.Tests.MarketNews;

[Collection(nameof(MarketNewsApiTestFixture))]
public class MarketNewsApiTest
{
    private readonly MarketNewsApiTestFixture _fixture;

    public MarketNewsApiTest(MarketNewsApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "ShouldFetchMarketNewsSuccessfully")]
    [Trait("Integration/Infra.ExternalServices", "MarketNews - API")]
    public async Task ShouldFetchMarketNewsSuccessfully()
    {
        var ticker = _fixture.GetRandomTicker();
        var response = await _fixture.Client.GetAsync($"{_fixture.BaseUrl}?function=NEWS_SENTIMENT&tickers={ticker}&apikey={_fixture.ApiKey}");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(content);
        if (result == null) throw new InvalidOperationException("Deserialized result is null.");


        ((string)result["items"]).Should().NotBeNullOrEmpty();
        foreach (var item in result["feed"])
        {
            ((string)item["title"]).Should().NotBeNullOrEmpty();
            ((string)item["url"]).Should().NotBeNullOrEmpty();
            var dateTimeString = (string)item["time_published"];
            var dateTime = DateTime.ParseExact(dateTimeString, "yyyyMMddTHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            dateTime.Should().BeBefore(DateTime.UtcNow);
            ((string)item["summary"]).Should().NotBeNullOrEmpty();
            ((string)item["source"]).Should().NotBeNullOrEmpty();
        }
    }

}
