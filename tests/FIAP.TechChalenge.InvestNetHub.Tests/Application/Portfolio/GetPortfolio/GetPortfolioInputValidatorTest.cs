using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.GetPortfolio;
using FluentAssertions;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.GetPortfolio;
[Collection(nameof(GetPortfolioTestFixture))]
public class GetPortfolioInputValidatorTest
{
    private readonly GetPortfolioTestFixture _fixture;

    public GetPortfolioInputValidatorTest(GetPortfolioTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ValidationOk))]
    [Trait("Application", "GetPortfolio - Use Cases")]
    public void ValidationOk()
    {
        var validInput = new GetPortfolioInput(Guid.NewGuid());
        var validator = new GetPortfolioInputValidator();

        var result = validator.Validate(validInput);

        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = nameof(ValidationExceptionWhenIdIsEmpty))]
    [Trait("Application", "GetPortfolio - Use Cases")]
    public void ValidationExceptionWhenIdIsEmpty()
    {
        var invalidInput = new GetPortfolioInput(Guid.Empty);
        var validator = new GetPortfolioInputValidator();

        var result = validator.Validate(invalidInput);

        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
    }
}
