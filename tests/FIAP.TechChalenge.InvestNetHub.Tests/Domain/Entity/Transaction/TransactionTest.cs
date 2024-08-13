using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FluentAssertions;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Domain.Entity.Transaction
{
    public class TransactionTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Transaction - Entity")]
        public void Instantiate()
        {
            var validData = new
            {
                PortfolioId = Guid.NewGuid(),
                AssetId = Guid.NewGuid(),
                Type = TransactionType.Buy,
                Quantity = 100,
                Price = 150.25m,
                TransactionDate = DateTime.UtcNow
            };

            var transaction = new DomainEntity.Transaction(
                validData.PortfolioId,
                validData.AssetId,
                validData.Type,
                validData.Quantity,
                validData.Price,
                validData.TransactionDate
            );

            transaction.Should().NotBeNull();
            transaction.PortfolioId.Should().Be(validData.PortfolioId);
            transaction.AssetId.Should().Be(validData.AssetId);
            transaction.Type.Should().Be(validData.Type);
            transaction.Quantity.Should().Be(validData.Quantity);
            transaction.Price.Should().Be(validData.Price);
            transaction.TransactionDate.Should().Be(validData.TransactionDate);
            transaction.Id.Should().NotBeEmpty();
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenQuantityIsInvalid))]
        [Trait("Domain", "Transaction - Entity")]
        [InlineData(0)]
        [InlineData(-1)]
        public void InstantiateErrorWhenQuantityIsInvalid(int quantity)
        {
            Action action = () => new DomainEntity.Transaction(
                Guid.NewGuid(),
                Guid.NewGuid(),
                TransactionType.Buy,
                quantity,
                150.25m,
                DateTime.UtcNow
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Quantity should be greater than 0");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenPriceIsInvalid))]
        [Trait("Domain", "Transaction - Entity")]
        [InlineData(0)]
        [InlineData(-10)]
        public void InstantiateErrorWhenPriceIsInvalid(decimal price)
        {
            Action action = () => new DomainEntity.Transaction(
                Guid.NewGuid(),
                Guid.NewGuid(),
                TransactionType.Buy,
                100,
                price,
                DateTime.UtcNow
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Price should be greater than 0");
        }
    }
}
