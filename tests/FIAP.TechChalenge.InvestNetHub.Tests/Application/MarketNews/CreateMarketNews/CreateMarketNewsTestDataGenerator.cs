namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.MarketNews.CreateMarketNews;
public class CreateMarketNewsTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new CreateMarketNewsTestFixture();
        var invalidInputList = new List<object[]>();


        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputEmptyTitle(),
            "Title cannot be empty or null."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputNullTitle(),
            "Title cannot be empty or null."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputEmptySummary(),
            "Summary cannot be empty or null."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputNullSummary(),
            "Summary cannot be empty or null."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputEmptyUrl(),
            "Url cannot be empty or null."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputNullUrl(),
            "Url cannot be empty or null."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputEmptySource(),
            "Source cannot be empty or null."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputNullSource(),
            "Source cannot be empty or null."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputEmptyAuthors(),
            "Authors list cannot be empty."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputNullAuthors(),
            "Authors list cannot be empty."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputNullOverallSentimentLabel(),
            "OverallSentimentLabel cannot be empty or null."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputEmptyOverallSentimentLabel(),
            "OverallSentimentLabel cannot be empty or null."
        });

        invalidInputList.Add(new object[]
        {
            fixture.GetInvalidInputInvalidPublishDate(),
            "PublishDate date cannot be in the future."
        });

        return invalidInputList;
    }
}
