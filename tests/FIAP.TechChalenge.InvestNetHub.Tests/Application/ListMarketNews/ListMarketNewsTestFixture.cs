using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using FIAP.TechChalenge.InvestNetHub.Tests.Common;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Common;
using Moq;

namespace FIAP.TechChalenge.InvestNetHub.Tests.Application.ListMarketNews;

[CollectionDefinition(nameof(ListMarketNewsTestFixture))]
public class ListMarketNewsTextFixtureCollection
    : ICollectionFixture<ListMarketNewsTestFixture>
{ }

public class ListMarketNewsTestFixture 
    : MarketNewsUseCasesBaseFixture
{

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


    public ListMarketNewsInput GetExampleInput()
    {
        var random = new Random();
        return new ListMarketNewsInput(
            page: random.Next(1, 10),
            perPage: random.Next(15, 100),
            search: Faker.Commerce.ProductName(),
            sort: Faker.Commerce.ProductName(),
            dir: random.Next(0, 10) > 5 ? SearchOrder.Asc : SearchOrder.Desc
        );
    }

}
