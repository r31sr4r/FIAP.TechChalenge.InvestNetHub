using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.Common;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.ListPortfolio;

[CollectionDefinition(nameof(ListPortfoliosTestFixture))]
public class ListPortfoliosTestFixtureCollection
    : ICollectionFixture<ListPortfoliosTestFixture>
{ }

public class ListPortfoliosTestFixture
    : PortfolioUseCasesBaseFixture
{
    public List<DomainEntity.Portfolio> GetExamplePortfoliosListWithNames(List<string> names)
    => names.Select(name => new DomainEntity.Portfolio(
        GetValidUserId(),
        name,
        GetValidPortfolioDescription()
    )).ToList();

    public List<DomainEntity.Portfolio> SortList(
    List<DomainEntity.Portfolio> portfoliosList,
    string orderBy,
    SearchOrder order
)
    {
        var listClone = new List<DomainEntity.Portfolio>(portfoliosList);
        var orderedEnumerable = (orderBy, order) switch
        {
            ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name).ToList(),
            ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name).ToList(),
            ("createdAt", SearchOrder.Asc) => listClone.OrderBy(x => x.CreatedAt).ToList(),
            ("createdAt", SearchOrder.Desc) => listClone.OrderByDescending(x => x.CreatedAt).ToList(),
            _ => listClone.OrderBy(x => x.Name).ToList(),
        };

        return orderedEnumerable.ToList();
    }
}
