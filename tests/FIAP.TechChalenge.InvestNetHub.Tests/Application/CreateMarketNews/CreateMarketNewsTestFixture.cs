using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Tests.Common;
using Moq;

namespace FIAP.TechChalenge.InvestNetHub.Tests.Application.CreateMarketNews;

[CollectionDefinition(nameof(CreateMarketNewsTestFixture))]
public class CreateMarketNewsTestFixtureCollection 
    : ICollectionFixture<CreateMarketNewsTestFixture>
{ }

public class CreateMarketNewsTestFixture : BaseFixture
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

    public string GetValidTitle()
        => Faker.Name.Random.String2(10);

    public string GetValidSummary()
        => Faker.Lorem.Paragraph();

    public DateTime GetValidPublishDate()
        => Faker.Date.Past();

    public string GetValidUrl()
        => Faker.Internet.Url();

    public string GetValidSource()
        => Faker.Name.Random.String2(10);
    public string GetValidImageUrl()
    => Faker.Internet.Url();

    public List<string> GetValidAuthors()
    {
        int authorsCount = Faker.Random.Int(1, 10);
        List<string> authors = new List<string>();
        for (int i = 0; i < authorsCount; i++)
        {
            authors.Add(GetValidAuthor());
        }
        return authors;

    }

    public string GetValidAuthor()
        => Faker.Name.Random.String2(10);

    public decimal GetValidOverallSentimentScore()
        => Faker.Random.Decimal(0, 1);

    public string GetValidOverallSentimentLabel()
        => Faker.Name.Random.String2(10);

    public Mock<IMarketNewsRepository> GetRepositoryMock()
        => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock()
        => new();

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


