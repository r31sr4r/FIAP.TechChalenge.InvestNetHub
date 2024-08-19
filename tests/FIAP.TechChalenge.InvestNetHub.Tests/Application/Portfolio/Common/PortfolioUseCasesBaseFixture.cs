using Bogus;
using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Common;
using Moq;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.Common;
public class PortfolioUseCasesBaseFixture
    : BaseFixture
{
    public Mock<IPortfolioRepository> GetRepositoryMock() => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    public DomainEntity.Portfolio GetValidPortfolio()
    {
        return new DomainEntity.Portfolio(
            Faker.Random.Guid().ToString(),
            Faker.Company.CompanyName(),
            Faker.Lorem.Sentence()
        );
    }

    public List<DomainEntity.Portfolio> GetPortfoliosList(int length = 10)
    {
        return Enumerable.Range(1, length)
            .Select(_ => GetValidPortfolio())
            .ToList();
    }
    public string GetValidPortfolioName()
    {
        var portfolioName = Faker.Company.CompanyName();
        if (portfolioName.Length > 255)
            portfolioName = portfolioName.Substring(0, 255);
        return portfolioName;
    }

    public string GetValidPortfolioDescription()
    {
        return Faker.Lorem.Sentence();
    }
}
