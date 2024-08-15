using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.DeletePortfolio;

[CollectionDefinition(nameof(DeletePortfolioTestFixture))]
public class DeletePortfolioTestFixtureCollection
    : ICollectionFixture<DeletePortfolioTestFixture>
{ }

public class DeletePortfolioTestFixture
    : PortfolioUseCasesBaseFixture
{ }
