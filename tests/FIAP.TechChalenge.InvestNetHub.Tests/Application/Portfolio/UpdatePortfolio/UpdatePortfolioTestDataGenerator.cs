using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.UpdatePortfolio;

public class UpdatePortfolioTestDataGenerator
{
    public static IEnumerable<object[]> GetPortfoliosToUpdate(int times = 10)
    {
        var fixture = new UpdatePortfolioTestFixture();
        for (int i = 0; i < times; i++)
        {
            var examplePortfolio = fixture.GetValidPortfolio();
            var exampleInput = fixture.GetValidInput(examplePortfolio.Id);
            yield return new object[] { examplePortfolio, exampleInput };
        }
    }

    public static IEnumerable<object[]> GetInvalidInputs(int numberOfIterations = 12)
    {
        var fixture = new UpdatePortfolioTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 3;

        for (int index = 0; index < numberOfIterations; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(new object[]
                    {
                    fixture.GetInvalidInputShortName(),
                    "Name should be greater than 3 characters"
                });
                    break;
                case 1:
                    invalidInputsList.Add(new object[]
                    {
                    fixture.GetInvalidInputTooLongName(),
                    "Name should be less than 255 characters"
                });
                    break;
                case 2:
                    invalidInputsList.Add(new object[]
                    {
                    fixture.GetInvalidInputWithoutName(),
                    "Name should not be empty or null"
                });
                    break;
            }
        }
        return invalidInputsList;
    }
}
