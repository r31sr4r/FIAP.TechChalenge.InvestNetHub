using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.UpdatePortfolio;
[CollectionDefinition(nameof(UpdatePortfolioTestFixture))]
public class UpdatePortfolioTestFixtureCollection
    : ICollectionFixture<UpdatePortfolioTestFixture>
{ }

public class UpdatePortfolioTestFixture
    : PortfolioUseCasesBaseFixture
{

    public UpdatePortfolioInput GetValidInput(Guid? id = null)
        => new UpdatePortfolioInput(
                id ?? Guid.NewGuid(),
                GetValidPortfolioName(),
                GetValidPortfolioDescription()
    );

    public UpdatePortfolioInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetValidInput();
        invalidInputShortName.Name = invalidInputShortName.Name[..2];

        return invalidInputShortName;
    }

    public UpdatePortfolioInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetValidInput();

        while (invalidInputTooLongName.Name.Length <= 255)
            invalidInputTooLongName.Name = $"{invalidInputTooLongName.Name} {Faker.Commerce.ProductName}";

        return invalidInputTooLongName;
    }

    public UpdatePortfolioInput GetInvalidInputWithoutName()
    {
        var invalidInputWithoutName = GetValidInput();
        invalidInputWithoutName.Name = string.Empty;
        return invalidInputWithoutName;
    }
}
