using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.CreatePortfolio;

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
        var input = GetInput();
        input.Name = input.Name[..2];
        return input;
    }

    public CreatePortfolioInput GetInvalidInputTooLongName()
    {
        var input = GetInput();
        while (input.Name.Length <= 255)
            input.Name = $"{input.Name} {Faker.Commerce.ProductName()}";

        return input;
    }
}
