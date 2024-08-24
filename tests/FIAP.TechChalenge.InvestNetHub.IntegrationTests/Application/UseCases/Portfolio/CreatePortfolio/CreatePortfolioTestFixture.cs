using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;
using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.CreatePortfolio;

[CollectionDefinition(nameof(CreatePortfolioTestFixture))]
public class CreatePortfolioTestFixtureCollection
    : ICollectionFixture<CreatePortfolioTestFixture>
{ }

public class CreatePortfolioTestFixture
    : PortfolioUseCasesBaseFixture
{
    public CreatePortfolioInput GetInput()
    {
        var portfolio = GetValidPortfolio();
        return new CreatePortfolioInput(
            portfolio.UserId,
            portfolio.Name,
            portfolio.Description
        );
    }

    public CreatePortfolioInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetInput();
        invalidInputShortName.Name =
            invalidInputShortName.Name[..2];

        return invalidInputShortName;
    }

    public CreatePortfolioInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetInput();

        while (invalidInputTooLongName.Name.Length <= 255)
            invalidInputTooLongName.Name = $"{invalidInputTooLongName.Name} {Faker.Commerce.ProductName}";

        return invalidInputTooLongName;
    }
}