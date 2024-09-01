using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddAssetToPortfolio;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using Bogus;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.AddAssetToPortfolio;

[CollectionDefinition(nameof(AddAssetToPortfolioTestFixture))]
public class AddAssetToPortfolioTestFixtureCollection : ICollectionFixture<AddAssetToPortfolioTestFixture> { }

public class AddAssetToPortfolioTestFixture : PortfolioUseCasesBaseFixture
{
    public AddAssetToPortfolioInput GetValidInput(Guid portfolioId)
    {
        var asset = GetValidAsset();
        return new AddAssetToPortfolioInput(
            portfolioId,
            asset.Name,
            asset.Code,
            asset.Type.ToString(),
            asset.Quantity,
            asset.Price
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


    public AddAssetToPortfolioInput GetInputWithInvalidType(Guid portfolioId)
    {
        return new AddAssetToPortfolioInput(
            portfolioId,
            Faker.Commerce.ProductName(),
            Faker.Random.AlphaNumeric(4).ToUpper(),
            "InvalidType", // Tipo inválido fixo
            Faker.Random.Int(1, 100),
            Faker.Random.Decimal(1, 1000)
        );
    }


    public AddAssetToPortfolioInput GetInputWithNegativeQuantity(Guid portfolioId)
    {
        var asset = GetValidAsset();
        return new AddAssetToPortfolioInput(
            portfolioId,
            asset.Name,              
            asset.Code,              
            asset.Type.ToString(),   
            -10,                     
            asset.Price             
        );
    }

    public AddAssetToPortfolioInput GetInputWithNegativePrice(Guid portfolioId)
    {
        var asset = GetValidAsset();
        return new AddAssetToPortfolioInput(
            portfolioId,
            asset.Name,              
            asset.Code,             
            asset.Type.ToString(),   
            asset.Quantity,          
            -100.5m                  
        );
    }


    public AddAssetToPortfolioInput GetInputWithInvalidName(Guid portfolioId)
    {
        return new AddAssetToPortfolioInput(
            portfolioId,
            string.Empty, // Nome inválido
            Faker.Random.AlphaNumeric(4).ToUpper(),
            AssetType.Stock.ToString(),
            Faker.Random.Int(1, 100),
            Faker.Random.Decimal(1, 1000)
        );
    }

}
