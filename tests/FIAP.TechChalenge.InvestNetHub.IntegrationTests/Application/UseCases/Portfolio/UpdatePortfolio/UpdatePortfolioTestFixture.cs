using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.UpdatePortfolio;

[CollectionDefinition(nameof(UpdatePortfolioTestFixture))]
public class UpdatePortfolioTestFixtureCollection
    : ICollectionFixture<UpdatePortfolioTestFixture>
{ }

public class UpdatePortfolioTestFixture
    : PortfolioUseCasesBaseFixture
{ }
