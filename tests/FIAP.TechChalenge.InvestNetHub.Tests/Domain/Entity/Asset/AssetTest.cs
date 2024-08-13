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
                Type = AssetType.Stock,
                Name = "Apple Inc.",
                Code = "AAPL"
            };

            var asset = new DomainEntity.Asset(validData.Type, validData.Name, validData.Code);

            asset.Should().NotBeNull();
            asset.Type.Should().Be(validData.Type);
            asset.Name.Should().Be(validData.Name);
            asset.Code.Should().Be(validData.Code);
            asset.Id.Should().NotBeEmpty();
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Asset - Entity")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            Action action = () => new DomainEntity.Asset(AssetType.Stock, name!, "AAPL");

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
            Action action = () => new DomainEntity.Asset(AssetType.Stock, "Apple Inc.", code!);

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
            Action action = () => new DomainEntity.Asset(AssetType.Stock, invalidName, "AAPL");

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be greater than 3 characters");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Asset - Entity")]
        public void InstantiateErrorWhenNameIsGreaterThan255Characters()
        {
            var invalidName = new string('a', 256);

            Action action = () => new DomainEntity.Asset(AssetType.Stock, invalidName, "AAPL");

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be less than 255 characters");
        }
    }
}
