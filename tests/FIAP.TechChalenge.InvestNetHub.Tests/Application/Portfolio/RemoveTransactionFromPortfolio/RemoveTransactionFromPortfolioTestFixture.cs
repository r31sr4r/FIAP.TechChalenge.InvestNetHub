using Bogus;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.RemoveTransactionFromPortfolio;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.Common;
using Xunit;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.RemoveTransactionFromPortfolio;

[CollectionDefinition(nameof(RemoveTransactionFromPortfolioTestFixture))]
public class RemoveTransactionFromPortfolioTestFixtureCollection : ICollectionFixture<RemoveTransactionFromPortfolioTestFixture> { }

public class RemoveTransactionFromPortfolioTestFixture : PortfolioUseCasesBaseFixture
{
    public RemoveTransactionFromPortfolioInput GetValidInput(Guid portfolioId, Guid transactionId)
    {
        return new RemoveTransactionFromPortfolioInput(portfolioId, transactionId);
    }

    public DomainEntity.Transaction GetValidTransaction(
        Guid portfolioId,
        Guid assetId
        )
    => new DomainEntity.Transaction(
        portfolioId,
        assetId,
        FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums.TransactionType.Buy,
        Faker.Random.Int(1, 100),
        Faker.Random.Decimal(1, 1000),
        DateTime.Now
    );

    public Asset GetValidAsset()
    {
        return new Asset(
            AssetType.Stock,
            Faker.Company.CompanyName(),
            Faker.Random.AlphaNumeric(4).ToUpper(),
            Faker.Random.Int(1, 100),
            Faker.Random.Decimal(1, 1000)
        );
    }
}
