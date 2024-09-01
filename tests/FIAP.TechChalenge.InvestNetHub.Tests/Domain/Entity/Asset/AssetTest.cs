using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FluentAssertions;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Domain.Entity.Asset
{
    public class AssetTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Asset - Entity")]
        public void Instantiate()
        {
            var validData = new
            {
                PortfolioId = Guid.NewGuid(),
                Type = AssetType.Stock,
                Name = "Apple Inc.",
                Code = "AAPL",
                Quantity = 50,
                Price = 150.25m
            };

            var asset = new DomainEntity.Asset(
                validData.PortfolioId,
                validData.Type, 
                validData.Name, 
                validData.Code, 
                validData.Quantity, 
                validData.Price
            );

            asset.Should().NotBeNull();
            asset.Type.Should().Be(validData.Type);
            asset.Name.Should().Be(validData.Name);
            asset.Code.Should().Be(validData.Code);
            asset.Quantity.Should().Be(validData.Quantity);
            asset.Price.Should().Be(validData.Price);
            asset.Id.Should().NotBeEmpty();
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Asset - Entity")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            Action action = () => new DomainEntity.Asset(
                Guid.NewGuid(),
                AssetType.Stock, 
                name!, 
                "AAPL", 
                50, 
                150.25m
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be empty or null");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenCodeIsEmpty))]
        [Trait("Domain", "Asset - Entity")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void InstantiateErrorWhenCodeIsEmpty(string? code)
        {
            Action action = () => new DomainEntity.Asset(
                Guid.NewGuid(),
                AssetType.Stock, 
                "Apple Inc.", 
                code!, 
                50, 
                150.25m
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Code should not be empty or null");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Asset - Entity")]
        [InlineData("Ap")]
        [InlineData("A")]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
        {
            Action action = () => new DomainEntity.Asset(
                Guid.NewGuid(),
                AssetType.Stock, 
                invalidName, 
                "AAPL", 
                50, 
                150.25m
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be greater than 3 characters");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Asset - Entity")]
        public void InstantiateErrorWhenNameIsGreaterThan255Characters()
        {
            var invalidName = new string('a', 256);

            Action action = () => new DomainEntity.Asset(Guid.NewGuid(),
                AssetType.Stock, 
                invalidName, 
                "AAPL", 
                50, 
                150.25m
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be less than 255 characters");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenQuantityIsInvalid))]
        [Trait("Domain", "Asset - Entity")]
        [InlineData(-1)]
        [InlineData(-10)]
        public void InstantiateErrorWhenQuantityIsInvalid(int quantity)
        {
            Action action = () => new DomainEntity.Asset(
                Guid.NewGuid(),
                AssetType.Stock, 
                "Apple Inc.", 
                "AAPL", 
                quantity, 
                150.25m
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Quantity should not be negative");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenPriceIsInvalid))]
        [Trait("Domain", "Asset - Entity")]
        [InlineData(0)]
        [InlineData(-10.5)]
        public void InstantiateErrorWhenPriceIsInvalid(decimal price)
        {
            Action action = () => new DomainEntity.Asset(
                Guid.NewGuid(),
                AssetType.Stock, 
                "Apple Inc.", 
                "AAPL", 
                50, 
                price
            );

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Price should be greater than 0");
        }
    }
}
