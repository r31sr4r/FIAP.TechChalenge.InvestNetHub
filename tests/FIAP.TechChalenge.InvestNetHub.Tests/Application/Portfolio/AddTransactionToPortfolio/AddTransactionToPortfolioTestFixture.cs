using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddTransactionToPortfolio;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using Bogus;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.AddTransactionToPortfolio;

[CollectionDefinition(nameof(AddTransactionToPortfolioTestFixture))]
public class AddTransactionToPortfolioTestFixtureCollection : ICollectionFixture<AddTransactionToPortfolioTestFixture> { }

public class AddTransactionToPortfolioTestFixture : PortfolioUseCasesBaseFixture
{
    public AddTransactionToPortfolioInput GetValidInput(Guid portfolioId, Guid assetId)
    {
        return new AddTransactionToPortfolioInput(
            portfolioId,
            assetId,
            TransactionType.Buy.ToString(),
            Faker.Random.Int(1, 100),
            Faker.Random.Decimal(1, 1000),
            DateTime.UtcNow
        );
    }

    public AddTransactionToPortfolioInput GetInputWithNegativeQuantity(Guid portfolioId, Guid assetId)
    {
        return new AddTransactionToPortfolioInput(
            portfolioId,
            assetId,
            TransactionType.Buy.ToString(),
            -10,
            Faker.Random.Decimal(1, 1000),
            DateTime.UtcNow
        );
    }

    public AddTransactionToPortfolioInput GetInputWithNegativePrice(Guid portfolioId, Guid assetId)
    {
        return new AddTransactionToPortfolioInput(
            portfolioId,
            assetId,
            TransactionType.Buy.ToString(),
            Faker.Random.Int(1, 100),
            -100.5m,
            DateTime.UtcNow
        );
    }

    public AddTransactionToPortfolioInput GetInputWithInvalidType(Guid portfolioId, Guid assetId)
    {
        return new AddTransactionToPortfolioInput(
            portfolioId,
            assetId,
            "InvalidType", // Tipo inválido fixo
            Faker.Random.Int(1, 100),
            Faker.Random.Decimal(1, 1000),
            DateTime.UtcNow
        );
    }

    public Asset GetValidAsset()
    {
        return new Asset(
            Guid.NewGuid(),
            AssetType.Stock,
            Faker.Company.CompanyName(),
            Faker.Random.AlphaNumeric(4).ToUpper(),
            Faker.Random.Int(1, 100),
            Faker.Random.Decimal(1, 1000)
        );
    }
}
