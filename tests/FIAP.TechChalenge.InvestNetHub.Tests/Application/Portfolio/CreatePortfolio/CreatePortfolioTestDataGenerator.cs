namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.CreatePortfolio;
public class CreatePortfolioTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int numberOfIterations = 12)
    {
        var fixture = new CreatePortfolioTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 2;

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
            }
        }
        return invalidInputsList;
    }
}
