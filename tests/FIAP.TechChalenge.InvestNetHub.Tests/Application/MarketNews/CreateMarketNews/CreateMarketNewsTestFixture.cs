using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Common;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.MarketNews.CreateMarketNews;

[CollectionDefinition(nameof(CreateMarketNewsTestFixture))]
public class CreateMarketNewsTestFixtureCollection
    : ICollectionFixture<CreateMarketNewsTestFixture>
{ }

public class CreateMarketNewsTestFixture
    : MarketNewsUseCasesBaseFixture
{
    public CreateMarketNewsInput GetInput()
    => new(
        GetValidTitle(),
        GetValidSummary(),
        GetValidPublishDate(),
        GetValidUrl(),
        GetValidSource(),
        GetValidImageUrl(),
        GetValidAuthors(),
        GetValidOverallSentimentScore(),
        GetValidOverallSentimentLabel()
    );

    public CreateMarketNewsInput GetInvalidInputEmptyTitle()
    {
        var invalidInputEmptyTitle = GetInput();
        invalidInputEmptyTitle.Title = string.Empty;
        return invalidInputEmptyTitle;
    }

    public CreateMarketNewsInput GetInvalidInputNullTitle()
    {
        var invalidInputNullTitle = GetInput();
        invalidInputNullTitle.Title = null!;
        return invalidInputNullTitle;
    }

    public CreateMarketNewsInput GetInvalidInputEmptySummary()
    {
        var invalidInputEmptySummary = GetInput();
        invalidInputEmptySummary.Summary = string.Empty;
        return invalidInputEmptySummary;
    }

    public CreateMarketNewsInput GetInvalidInputNullSummary()
    {
        var invalidInputNullSummary = GetInput();
        invalidInputNullSummary.Summary = null!;
        return invalidInputNullSummary;
    }

    public CreateMarketNewsInput GetInvalidInputEmptyUrl()
    {
        var invalidInputEmptyUrl = GetInput();
        invalidInputEmptyUrl.Url = string.Empty;
        return invalidInputEmptyUrl;
    }

    public CreateMarketNewsInput GetInvalidInputNullUrl()
    {
        var invalidInputNullUrl = GetInput();
        invalidInputNullUrl.Url = null!;
        return invalidInputNullUrl;
    }

    public CreateMarketNewsInput GetInvalidInputEmptySource()
    {
        var invalidInputEmptySource = GetInput();
        invalidInputEmptySource.Source = string.Empty;
        return invalidInputEmptySource;
    }

    public CreateMarketNewsInput GetInvalidInputNullSource()
    {
        var invalidInputNullSource = GetInput();
        invalidInputNullSource.Source = null!;
        return invalidInputNullSource;
    }

    public CreateMarketNewsInput GetInvalidInputEmptyAuthors()
    {
        var invalidInputEmptyAuthors = GetInput();
        invalidInputEmptyAuthors.Authors = new List<string>();
        return invalidInputEmptyAuthors;
    }

    public CreateMarketNewsInput GetInvalidInputNullAuthors()
    {
        var invalidInputNullAuthors = GetInput();
        invalidInputNullAuthors.Authors = null!;
        return invalidInputNullAuthors;
    }

    public CreateMarketNewsInput GetInvalidInputEmptyOverallSentimentLabel()
    {
        var invalidInputEmptyOverallSentimentLabel = GetInput();
        invalidInputEmptyOverallSentimentLabel.OverallSentimentLabel = string.Empty;
        return invalidInputEmptyOverallSentimentLabel;
    }

    public CreateMarketNewsInput GetInvalidInputNullOverallSentimentLabel()
    {
        var invalidInputNullOverallSentimentLabel = GetInput();
        invalidInputNullOverallSentimentLabel.OverallSentimentLabel = null!;
        return invalidInputNullOverallSentimentLabel;
    }
    public CreateMarketNewsInput GetInvalidInputInvalidPublishDate()
    {
        var invalidInputDateInTheFuture = GetInput();
        invalidInputDateInTheFuture.PublishDate = DateTime.Now.AddDays(1);
        return invalidInputDateInTheFuture;
    }




}


