using Bogus;
using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Base;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.Common;
public class PortfolioUseCasesBaseFixture
    : BaseFixture
{
    public string GetValidPortfolioName()
    {
        var portfolioName = "";
        while (portfolioName.Length < 3)
            portfolioName = Faker.Company.CompanyName();
        if (portfolioName.Length > 255)
            portfolioName = portfolioName[..255];
        return portfolioName;
    }

    public string GetValidPortfolioDescription()
        => Faker.Lorem.Sentence();

    public DomainEntity.Portfolio GetValidPortfolio()
        => new(
            Faker.Random.Guid().ToString(),
            GetValidPortfolioName(),
            GetValidPortfolioDescription()
        );

    public List<DomainEntity.Portfolio> GetPortfoliosList(int length = 10)
    {
        return Enumerable.Range(1, length)
            .Select(_ => GetValidPortfolio())
            .ToList();
    }

    public string GetValidUserId()
    {
        return Faker.Random.Guid().ToString();
    }
}
