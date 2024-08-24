using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Domain.Entity.Portfolio
{
    public class PortfolioTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Portfolio - AggregateRoot")]
        public void Instantiate()
        {
            var validData = new
            {
                UserId = Guid.NewGuid(),
                Name = "My Portfolio",
                Description = "This is a test portfolio"
            };

            var portfolio = new DomainEntity.Portfolio(validData.UserId, validData.Name, validData.Description);

            portfolio.Should().NotBeNull();
            portfolio.UserId.Should().Be(validData.UserId);
            portfolio.Name.Should().Be(validData.Name);
            portfolio.Description.Should().Be(validData.Description);
            portfolio.Id.Should().NotBeEmpty();
            portfolio.Assets.Should().BeEmpty();
            portfolio.Transactions.Should().BeEmpty();
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsInvalid))]
        [Trait("Domain", "Portfolio - AggregateRoot")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void InstantiateErrorWhenNameIsInvalid(string? name)
        {
            Action action = () => new DomainEntity.Portfolio(
                Guid.NewGuid(),
                name!,
                "This is a test portfolio"
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be empty or null");
        }

[Fact(DisplayName = nameof(AddAssetSuccessfully))]
        [Trait("Domain", "Portfolio - Methods")]
        public void AddAssetSuccessfully()
        {
            var portfolio = CreateValidPortfolio();
            var asset = CreateValidAsset();

            portfolio.AddAsset(asset);

            portfolio.Assets.Should().ContainSingle();
            portfolio.Assets.First().Should().Be(asset);
        }

        [Fact(DisplayName = nameof(AddAssetThrowsWhenAssetAlreadyExists))]
        [Trait("Domain", "Portfolio - Methods")]
        public void AddAssetThrowsWhenAssetAlreadyExists()
        {
            var portfolio = CreateValidPortfolio();
            var asset = CreateValidAsset();

            portfolio.AddAsset(asset);

            Action action = () => portfolio.AddAsset(asset);

            action.Should()
                .Throw<BusinessRuleException>()
                .WithMessage($"The asset with code {asset.Code} already exists in the portfolio.");
        }

        [Fact(DisplayName = nameof(RemoveAssetSuccessfully))]
        [Trait("Domain", "Portfolio - Methods")]
        public void RemoveAssetSuccessfully()
        {
            var portfolio = CreateValidPortfolio();
            var asset = CreateValidAsset();

            portfolio.AddAsset(asset);
            portfolio.RemoveAsset(asset);

            portfolio.Assets.Should().BeEmpty();
        }

        [Fact(DisplayName = nameof(RemoveAssetThrowsWhenAssetHasTransactions))]
        [Trait("Domain", "Portfolio - Methods")]
        public void RemoveAssetThrowsWhenAssetHasTransactions()
        {
            var portfolio = CreateValidPortfolio();
            var asset = CreateValidAsset();
            var transaction = CreateValidTransaction(portfolio.Id, asset.Id);

            portfolio.AddAsset(asset);
            portfolio.AddTransaction(transaction);

            Action action = () => portfolio.RemoveAsset(asset);

            action.Should()
                .Throw<BusinessRuleException>()
                .WithMessage($"The asset with code {asset.Code} cannot be removed as it has associated transactions.");
        }

        [Fact(DisplayName = nameof(AddTransactionSuccessfully))]
        [Trait("Domain", "Portfolio - Methods")]
        public void AddTransactionSuccessfully()
        {
            var portfolio = CreateValidPortfolio();
            var asset = CreateValidAsset();
            var transaction = CreateValidTransaction(portfolio.Id, asset.Id);

            portfolio.AddAsset(asset);
            portfolio.AddTransaction(transaction);

            portfolio.Transactions.Should().ContainSingle();
            portfolio.Transactions.First().Should().Be(transaction);
        }

        [Fact(DisplayName = nameof(AddTransactionThrowsWhenAssetDoesNotExist))]
        [Trait("Domain", "Portfolio - Methods")]
        public void AddTransactionThrowsWhenAssetDoesNotExist()
        {
            var portfolio = CreateValidPortfolio();
            var transaction = CreateValidTransaction(portfolio.Id, Guid.NewGuid());

            Action action = () => portfolio.AddTransaction(transaction);

            action.Should()
                .Throw<BusinessRuleException>()
                .WithMessage($"Cannot add transaction. Asset with ID {transaction.AssetId} not found in portfolio.");
        }

        [Fact(DisplayName = nameof(RemoveTransactionSuccessfully))]
        [Trait("Domain", "Portfolio - Methods")]
        public void RemoveTransactionSuccessfully()
        {
            var portfolio = CreateValidPortfolio();
            var asset = CreateValidAsset();
            var transaction = CreateValidTransaction(portfolio.Id, asset.Id);

            portfolio.AddAsset(asset);
            portfolio.AddTransaction(transaction);

            portfolio.RemoveTransaction(transaction);

            portfolio.Transactions.Should().BeEmpty();
        }

        [Fact(DisplayName = nameof(RemoveTransactionThrowsWhenTransactionIsTooOld))]
        [Trait("Domain", "Portfolio - Methods")]
        public void RemoveTransactionThrowsWhenTransactionIsTooOld()
        {
            var portfolio = CreateValidPortfolio();
            var asset = CreateValidAsset();
            var oldTransaction = new DomainEntity.Transaction(
                portfolio.Id, 
                asset.Id, 
                TransactionType.Buy, 
                100, 
                150.25m, 
                DateTime.Now.AddMonths(-2)
            );

            portfolio.AddAsset(asset);
            portfolio.AddTransaction(oldTransaction);

            Action action = () => portfolio.RemoveTransaction(oldTransaction);

            action.Should()
                .Throw<BusinessRuleException>()
                .WithMessage($"Transaction from {oldTransaction.TransactionDate} cannot be removed.");
        }

        private DomainEntity.Portfolio CreateValidPortfolio()
        {
            return new DomainEntity.Portfolio(
                Guid.NewGuid(),
                "My Portfolio",
                "This is a test portfolio"
            );
        }

        private DomainEntity.Asset CreateValidAsset()
        {
            return new DomainEntity.Asset(
                AssetType.Stock,
                "Apple Inc.",
                "AAPL"
            );
        }

        private DomainEntity.Transaction CreateValidTransaction(Guid portfolioId, Guid assetId)
        {
            return new DomainEntity.Transaction(
                portfolioId,
                assetId,
                TransactionType.Buy,
                100,
                150.25m,
                DateTime.UtcNow
            );
        }
    }
}
