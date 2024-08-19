using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.ListPortfolios;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.Common;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.ListPortfolios;

[CollectionDefinition(nameof(ListPortfoliosTestFixture))]
public class ListPortfoliosTestFixtureCollection
    : ICollectionFixture<ListPortfoliosTestFixture>
{ }

public class ListPortfoliosTestFixture
    : PortfolioUseCasesBaseFixture
{
    public List<DomainEntity.Portfolio> GetPortfoliosList(int length = 10)
    {
        var list = new List<DomainEntity.Portfolio>();
        for (int i = 0; i < length; i++)
        {
            list.Add(GetValidPortfolio());
        }
        return list;
    }

    public ListPortfoliosInput GetInput()
    {
        var random = new Random();
        return new ListPortfoliosInput(
            page: random.Next(1, 10),
            perPage: random.Next(15, 100),
            search: Faker.Commerce.ProductName(),
            sort: Faker.Commerce.ProductName(),
            dir: random.Next(0, 10) > 5 ? SearchOrder.Asc : SearchOrder.Desc
        );
    }
}
