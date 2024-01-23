using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Tests.Common;
using Moq;

namespace FIAP.TechChalenge.InvestNetHub.Tests.Application.ListMarketNews;

[CollectionDefinition(nameof(ListMarketNewsTestFixture))]
public class ListMarketNewsTextFixtureCollection
    : ICollectionFixture<ListMarketNewsTestFixture>
{ }

public class ListMarketNewsTestFixture : BaseFixture
{
    public Mock<IMarketNewsRepository> GetRepositoryMock()
        => new();

    public List<MarketNews> GetExampleMarketNewsList(int lenght = 10)
    {
        var marketNewsList = new List<MarketNews>();
        for (int i = 0; i < lenght; i++)
        {
            marketNewsList.Add(GetExampleMarketNews());
        }

        return marketNewsList;

    }

    public MarketNews GetExampleMarketNews()
        => new(
            GetValidTitle(),
            GetValidSummary(),
            GetValidPublishDate(),
            GetValidUrl(),
            GetValidSource(),
            GetValidImageUrl(),
            GetValidAuthors(),
            GetValidOverallSentimentScore(),
            GetValidOverallSentimentLabel()
        );

    public string GetValidTitle()
        => Faker.Name.Random.String2(10);

    public string GetValidSummary()
        => Faker.Lorem.Paragraph();

    public DateTime GetValidPublishDate()
        => Faker.Date.Past();

    public string GetValidUrl()
        => Faker.Internet.Url();

    public string GetValidSource()
        => Faker.Name.Random.String2(10);
    public string GetValidImageUrl()
    => Faker.Internet.Url();

    public List<string> GetValidAuthors()
    {
        int authorsCount = Faker.Random.Int(1, 10);
        List<string> authors = new List<string>();
        for (int i = 0; i < authorsCount; i++)
        {
            authors.Add(GetValidAuthor());
        }
        return authors;

    }

    public string GetValidAuthor()
        => Faker.Name.Random.String2(10);

    public decimal GetValidOverallSentimentScore()
        => Faker.Random.Decimal(0, 1);

    public string GetValidOverallSentimentLabel()
        => Faker.Name.Random.String2(10);

}
