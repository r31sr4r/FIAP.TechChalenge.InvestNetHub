using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;
using FluentAssertions;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.UpdatePortfolio;
[Collection(nameof(UpdatePortfolioTestFixture))]
public class UpdatePortfolioInputValidatorTest
{
    private readonly UpdatePortfolioTestFixture fixture;

    public UpdatePortfolioInputValidatorTest(UpdatePortfolioTestFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact(DisplayName = nameof(DontAcceptWhenGuidIsEmpty))]
    [Trait("Application", "UpdatePortfolioInputValidator - Use Cases")]
    public void DontAcceptWhenGuidIsEmpty()
    {
        var input = fixture.GetValidInput(Guid.Empty);
        var validator = new UpdatePortfolioInputValidator();

        var validateResult = validator.Validate(input);

        validateResult.Should().NotBeNull();
        validateResult.IsValid.Should().BeFalse();
        validateResult.Errors.Should().HaveCount(1);
        validateResult.Errors[0].ErrorMessage
            .Should().Be("Id must not be empty");
    }

    [Fact(DisplayName = nameof(AcceptWhenGuidIsNotEmpty))]
    [Trait("Application", "UpdatePortfolioInputValidator - Use Cases")]
    public void AcceptWhenGuidIsNotEmpty()
    {
        var input = fixture.GetValidInput();
        var validator = new UpdatePortfolioInputValidator();

        var validateResult = validator.Validate(input);

        validateResult.Should().NotBeNull();
        validateResult.IsValid.Should().BeTrue();
        validateResult.Errors.Should().HaveCount(0);
    }
}
