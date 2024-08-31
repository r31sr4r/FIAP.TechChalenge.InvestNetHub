using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeleteAssetFromPortfolio;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.DeleteAssetFromPortfolio;

public class DeleteAssetFromPortfolioTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int numberOfIterations = 4)
    {
        var fixture = new DeleteAssetFromPortfolioTestFixture();
        var invalidInputsList = new List<object[]>();

        for (int index = 0; index < numberOfIterations; index++)
        {
            var portfolioId = Guid.NewGuid();
            var assetId = Guid.NewGuid();
            invalidInputsList.Add(new object[]
            {
                new DeleteAssetFromPortfolioInput(portfolioId, assetId),
                $"Asset with id {assetId} not found in portfolio {portfolioId}"
            });
        }
        return invalidInputsList;
    }
}
