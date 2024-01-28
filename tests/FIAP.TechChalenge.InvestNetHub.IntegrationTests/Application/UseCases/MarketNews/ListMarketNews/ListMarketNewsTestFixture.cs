using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Base;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.MarketNews.ListMarketNews;
[CollectionDefinition(nameof(ListMarketNewsTestFixture))]
public class ListMarketNewsTestFixtureCollection
    : ICollectionFixture<ListMarketNewsTestFixture>
{ }

public class ListMarketNewsTestFixture
    : BaseFixture
{
    public ListMarketNewsInput GetRandomInput()
    {
        var random = new Random();

        var tickers = GetRandomTicker();
        var topics = GetRandomTopics();

        DateTime fromTime = DateTime.UtcNow.AddDays(-random.Next(1, 30));
        DateTime toTime = fromTime.AddDays(random.Next(1, 30));

        string sort = GetRandomSort();
        int limit = GetRandomLimit();

        return new ListMarketNewsInput(
            tickers: tickers,
            topics: topics,
            fromTime: fromTime,
            toTime: toTime,
            sort: sort,
            limit: limit
        );
    }

    public string GetRandomTicker() => Faker.PickRandom(new List<string> { "IBM", "AAPL", "COIN:BTC", "FOREX:USD" });

    public string GetRandomTopics()
    {
        var topics = new List<string>
        {
            "blockchain", "earnings", "ipo", "mergers_and_acquisitions",
            "financial_markets", "economy_fiscal", "economy_monetary",
            "economy_macro", "energy_transportation", "finance", "life_sciences",
            "manufacturing", "real_estate", "retail_wholesale", "technology"
        };

        return Faker.PickRandom(topics);
    }

    public string GetRandomSort() => Faker.PickRandom(new List<string> { "LATEST", "EARLIEST", "RELEVANCE" });

    public int GetRandomLimit() => Faker.Random.Int(10, 1000);

    public ListMarketNewsInput GetInput()
    {
        return new ListMarketNewsInput(
            tickers: "AAPL",
            topics: string.Empty,
            fromTime: DateTime.Now.AddDays(-7),
            toTime: DateTime.Now,
            sort: "LATEST",
            limit: 10
        );
    }

}
