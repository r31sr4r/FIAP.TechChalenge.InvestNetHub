using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.ListPortfolios;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.ListPortfolios;
public class ListPortfoliosTestDataGenerator
{
    public static IEnumerable<object[]> GetInputWithoutAllParameters(int times = 18)
    {
        var fixture = new ListPortfoliosTestFixture();
        var inputExample = fixture.GetInput();
        for (int i = 0; i < times; i++)
        {
            switch (i % 6)
            {
                case 0:
                    yield return new object[] { new ListPortfoliosInput() };
                    break;
                case 1:
                    yield return new object[] { new ListPortfoliosInput(inputExample.Page) };
                    break;
                case 2:
                    yield return new object[] { new ListPortfoliosInput(
                        inputExample.Page,
                        inputExample.PerPage
                        ) };
                    break;
                case 3:
                    yield return new object[] { new ListPortfoliosInput(
                        inputExample.Page,
                        inputExample.PerPage,
                        inputExample.Search
                        ) };
                    break;
                case 4:
                    yield return new object[] { new ListPortfoliosInput(
                        inputExample.Page,
                        inputExample.PerPage,
                        inputExample.Search,
                        inputExample.Sort
                        ) };
                    break;
                case 5:
                    yield return new object[] { inputExample };
                    break;

                default:
                    yield return new object[] { new ListPortfoliosInput() };
                    break;
            }
        }
    }
}
