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

    public string GetRandomTicker() => _faker.PickRandom(new List<string> { "IBM", "AAPL", "COIN", "BTC", "USD" });

    public List<string> GetRandomTopics() => _faker.Random.Shuffle(new List<string>
    {
        "blockchain", "earnings", "ipo", "mergers_and_acquisitions",
        "financial_markets", "economy_fiscal", "economy_monetary",
        "economy_macro", "energy_transportation", "finance", "life_sciences",
        "manufacturing", "real_estate", "retail_wholesale", "technology"
    }).Take(_faker.Random.Int(1, 5)).ToList();

    public string GetValidTicker() => "AAPL";
    public string GetValidTopic() => "technology";
    public string GetValidTimeFrom() => "20220101T0000";
    public string GetValidTimeTo() => "20221231T2359";
    public string GetValidSort() => "LATEST";
    public int GetValidLimit() => 50;

}
