using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.MarketNews.ListMarketNews;
public class ListMarketNewsTestDataGenerator
{
    public static IEnumerable<object[]> GetInputWithoutAllParameters(int times = 15)
    {
        var fixture = new ListMarketNewsTestFixture();
        var inputExample = fixture.GetExampleInput();
        for (int i = 0; i < times; i++)
        {
            switch (i % 5)
            {
                case 0:
                    yield return new object[]
                    {
                        new ListMarketNewsInput()
                    };
                    break;
                case 1:
                    yield return new object[]
                    {
                        new ListMarketNewsInput(
                            page: inputExample.Page
                        )
                    };
                    break;
                case 2:
                    yield return new object[]
                    {
                        new ListMarketNewsInput(
                            page: inputExample.Page,
                            perPage: inputExample.PerPage
                        )
                    };
                    break;
                case 3:
                    yield return new object[]
                    {
                        new ListMarketNewsInput(
                            page: inputExample.Page,
                            perPage: inputExample.PerPage,
                            search: inputExample.Search
                        )
                    };
                    break;
                case 4:
                    yield return new object[]
                    {
                        new ListMarketNewsInput(
                            page: inputExample.Page,
                            perPage: inputExample.PerPage,
                            search: inputExample.Search,
                            sort: inputExample.Sort
                        )
                    };
                    break;
                case 5:
                    yield return new object[]
                    {
                        inputExample
                    };
                    break;

                default:
                    yield return new object[]
                    {
                        new ListMarketNewsInput()
                    };
                    break;
            }
        }




    }


}
