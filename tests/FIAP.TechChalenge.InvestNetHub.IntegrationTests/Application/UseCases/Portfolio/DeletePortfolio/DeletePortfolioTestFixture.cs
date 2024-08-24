using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.DeletePortfolio;

[CollectionDefinition(nameof(DeletePortfolioTestFixture))]
public class DeletePortfolioTestFixtureCollection
    : ICollectionFixture<DeletePortfolioTestFixture>
{ }

public class DeletePortfolioTestFixture
    : PortfolioUseCasesBaseFixture
{ }
