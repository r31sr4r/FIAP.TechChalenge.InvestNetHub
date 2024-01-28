using Bogus;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Models;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Common;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.MarketNews.ListMarketNews;

[CollectionDefinition(nameof(ListMarketNewsTestFixture))]
public class ListMarketNewsTextFixtureCollection
    : ICollectionFixture<ListMarketNewsTestFixture>
{ }

public class ListMarketNewsTestFixture
    : MarketNewsUseCasesBaseFixture
{

    public List<MarketNewsDto> GetExampleMarketNewsDtoList(int length = 10)
    {
        var marketNewsDtoList = new List<MarketNewsDto>();
        for (int i = 0; i < length; i++)
        {
            marketNewsDtoList.Add(GetExampleMarketNewsDto());
        }

        return marketNewsDtoList;
    }

    public MarketNewsDto GetExampleMarketNewsDto()
    {
        return new MarketNewsDto
        {
            Title = GetValidTitle(),
            Summary = GetValidSummary(),
            PublishDate = GetValidPublishDate(),
            Url = GetValidUrl(),
            Source = GetValidSource(),
            ImageUrl = GetValidImageUrl(),
            Authors = GetValidAuthors(),
            OverallSentimentScore = GetValidOverallSentimentScore(),
            OverallSentimentLabel = GetValidOverallSentimentLabel()
        };
    }

    public ListMarketNewsInput GetExampleInput()
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

}
