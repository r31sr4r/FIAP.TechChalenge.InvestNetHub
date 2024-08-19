using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.GetPortfolio;

[CollectionDefinition(nameof(GetPortfolioTestFixture))]
public class GetPortfolioTestFixtureCollection :
    ICollectionFixture<GetPortfolioTestFixture>
{ }

public class GetPortfolioTestFixture
    : PortfolioUseCasesBaseFixture
{ }
