using Bogus;
using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.ExternalServices.Common;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.ExternalServices.Tests.MarketNews;

[CollectionDefinition(nameof(MarketNewsApiTestFixture))]
public class MarketNewsApiTestFixtureCollection : ICollectionFixture<MarketNewsApiTestFixture>
{ }

public class MarketNewsApiTestFixture
    : ApiClientBaseFixture
{
    private readonly Faker _faker = new Faker();

    public string GetRandomTicker() => _faker.PickRandom(new List<string> { "IBM", "AAPL", "COIN:BTC", "FOREX:USD" });

    public List<string> GetRandomTopics() => _faker.Random.Shuffle(new List<string>
    {
        "blockchain", "earnings", "ipo", "mergers_and_acquisitions",
        "financial_markets", "economy_fiscal", "economy_monetary",
        "economy_macro", "energy_transportation", "finance", "life_sciences",
        "manufacturing", "real_estate", "retail_wholesale", "technology"
    }).Take(_faker.Random.Int(1, 5)).ToList();

    public string GetRandomTimeFrom() => _faker.Date.Past(1).ToString("yyyyMMddTHHmm");

    public string GetRandomTimeTo() => _faker.Date.Future(1).ToString("yyyyMMddTHHmm");

    public string GetRandomSort() => _faker.PickRandom(new List<string> { "LATEST", "EARLIEST", "RELEVANCE" });

    public int GetRandomLimit() => _faker.Random.Int(10, 1000);

}
