using Bogus;
using Bogus.Extensions;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeleteAssetFromPortfolio;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.Common;
using Xunit;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.DeleteAssetFromPortfolio;

[CollectionDefinition(nameof(DeleteAssetFromPortfolioTestFixture))]
public class DeleteAssetFromPortfolioTestFixtureCollection : ICollectionFixture<DeleteAssetFromPortfolioTestFixture> { }

public class DeleteAssetFromPortfolioTestFixture : PortfolioUseCasesBaseFixture
{
    public DeleteAssetFromPortfolioInput GetValidInput(Guid portfolioId, Guid assetId)
    {
        return new DeleteAssetFromPortfolioInput(portfolioId, assetId);
    }

    public DomainEntity.Asset GetValidAsset(Guid portfolioId)
    => new(
        AssetType.Stock,
        GetValidName(),
        GetValidCode(),
        Faker.Random.Int(1, 100),
        Faker.Random.Decimal(1, 1000)
    );

     public string GetValidName()
    {
        // Gera um nome válido de ativo com pelo menos 4 caracteres e menos de 255 caracteres
        return new Faker().Company.CompanyName().ClampLength(4, 254);
    }

    public string GetValidCode()
    {
        // Gera um código válido, por exemplo, com 4 caracteres alfanuméricos
        return new Faker().Random.AlphaNumeric(4).ToUpper();
    }
}
