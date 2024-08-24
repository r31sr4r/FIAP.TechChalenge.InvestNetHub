using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.GetPortfolio;

[CollectionDefinition(nameof(GetPortfolioTestFixture))]
public class GetPortfolioTestFixtureCollection
    : ICollectionFixture<GetPortfolioTestFixture>
{ }

public class GetPortfolioTestFixture
    : PortfolioUseCasesBaseFixture
{ }
