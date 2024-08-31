using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Base;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.Data.EF.Repositories.PortfolioRepository;

[CollectionDefinition(nameof(PortfolioRepositoryTestFixture))]
public class PortfolioRepositoryTestFixtureCollection
    : ICollectionFixture<PortfolioRepositoryTestFixture>
{ }

public class PortfolioRepositoryTestFixture
    : BaseFixture
{
    public Portfolio GetValidPortfolio()
    {
        return new Portfolio(
            Guid.NewGuid(),
            "My Portfolio",
            "This is a test portfolio"
        );
    }

    public Asset GetValidAsset()
    {
        return new Asset(
            AssetType.Stock,
            "Apple Inc.",
            "AAPL",
            50,           
            150.25m       
        );
    }

    public Transaction GetValidTransaction(Guid portfolioId, Guid assetId)
    {
        return new Transaction(
            portfolioId,
            assetId,
            TransactionType.Buy,
            100,
            150.25m,
            DateTime.UtcNow
        );
    }

    public List<Portfolio> GetExamplePortfolioList(int length = 10)
    {
        return Enumerable.Range(1, length).Select(_ => GetValidPortfolio()).ToList();
    }

    public List<Asset> GetExampleAssetList(int length = 10)
    {
        return Enumerable.Range(1, length).Select(_ => GetValidAsset()).ToList();
    }

    public List<Transaction> GetExampleTransactionList(Guid portfolioId, Guid assetId, int length = 10)
    {
        return Enumerable.Range(1, length).Select(_ => GetValidTransaction(portfolioId, assetId)).ToList();
    }
}
