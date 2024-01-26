using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.AlphaVantage;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Common;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.ExternalServices.Tests.MarketNews;
public class AlphaVantageMarketNewsServiceTest
{
    [Fact(DisplayName = "ShouldReturnNewsList")]
    [Trait("Integration/Infra.ExternalServices", "MarketNews - Service")]
    public async Task ShouldReturnNewsList()
    {
        var logger = new LoggerFactory().CreateLogger<MarketNewsMapper>();
        var mapper = new MarketNewsMapper(logger);
        var service = new AlphaVantageMarketNewsService(mapper);

        var newsList = await service.GetMarketNewsAsync("AAPL", "", DateTime.Now.AddDays(-7), DateTime.Now, "LATEST", 10);

        newsList.Should().NotBeNullOrEmpty();
        newsList.Should().AllBeOfType<MarketNewsDto>();

        foreach (var news in newsList)
        {
            news.Title.Should().NotBeNullOrEmpty();
            news.PublishDate.Should().BeBefore(DateTime.UtcNow);
        }
    }
}
