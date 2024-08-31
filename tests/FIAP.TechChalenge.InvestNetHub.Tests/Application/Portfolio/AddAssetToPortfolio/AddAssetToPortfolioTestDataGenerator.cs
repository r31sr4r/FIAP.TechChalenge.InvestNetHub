namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.AddAssetToPortfolio;

public class AddAssetToPortfolioTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int numberOfIterations = 4)
    {
        var fixture = new AddAssetToPortfolioTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 4;

        for (int index = 0; index < numberOfIterations; index++)
        {
            var portfolioId = Guid.NewGuid();
            switch (index % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(new object[]
                    {
                        fixture.GetInputWithInvalidName(portfolioId),
                        "Name should not be empty or null"
                    });
                    break;
                case 1:
                    invalidInputsList.Add(new object[]
                    {
                        fixture.GetInputWithInvalidType(portfolioId),
                        "Invalid asset type: InvalidType"
                    });
                    break;
                case 2:
                    invalidInputsList.Add(new object[]
                    {
                        fixture.GetInputWithNegativeQuantity(portfolioId),
                        "Quantity should not be negative"
                    });
                    break;
                case 3:
                    invalidInputsList.Add(new object[]
                    {
                        fixture.GetInputWithNegativePrice(portfolioId),
                        "Price should be greater than 0"
                    });
                    break;
            }
        }
        return invalidInputsList;
    }
}
