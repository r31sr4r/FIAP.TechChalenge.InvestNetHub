using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Tests.Domain.Entity.MarketNews;
public class MarketNewsTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "MarketNews - Aggregates")]
    public void Instantiate()
    {
        var validMarketNews = new
        {
            Title = "Test",
            Summary = "Test Summary",
            PublishDate = DateTime.Now.AddDays(-2),
            Url = "http://www.teste.com.br",
            Source = "Test Source",
            ImageUrl = "http://www.imageurl.com",
            Authors = new List<string> { "Author1", "Author2" },
            OverallSentimentScore = 0.5m,
            OverallSentimentLabel = "Neutral"
        };

        var marketNews = new DomainEntity.MarketNews(
            validMarketNews.Title,
            validMarketNews.Summary,
            validMarketNews.PublishDate,
            validMarketNews.Url,
            validMarketNews.Source,
            validMarketNews.ImageUrl,
            validMarketNews.Authors,
            validMarketNews.OverallSentimentScore,
            validMarketNews.OverallSentimentLabel
        );

        Assert.NotNull(marketNews);
        Assert.NotEqual(default(Guid), marketNews.Id);
        Assert.Equal(validMarketNews.Title, marketNews.Title);
        Assert.Equal(validMarketNews.Summary, marketNews.Summary);
        Assert.Equal(validMarketNews.PublishDate, marketNews.PublishDate);
        Assert.Equal(validMarketNews.Url, marketNews.Url);
        Assert.Equal(validMarketNews.Source, marketNews.Source);
        Assert.Equal(validMarketNews.ImageUrl, marketNews.ImageUrl);
        Assert.Equal(validMarketNews.Authors, marketNews.Authors);
        Assert.Equal(validMarketNews.OverallSentimentScore, marketNews.OverallSentimentScore);
        Assert.Equal(validMarketNews.OverallSentimentLabel, marketNews.OverallSentimentLabel);

    }

    [Theory(DisplayName = nameof(Instantiate_InvalidTitle))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Instantiate_InvalidTitle(string? title)
    {
        Action action = () => new DomainEntity.MarketNews(
            title!,
            "Test Summary",
            DateTime.Now.AddDays(-2),
            "http://www.teste.com.br",
            "Test Source",
            "http://www.imageurl.com",
            new List<string> { "Author1", "Author2" },
            0.5m,
            "Neutral");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Title cannot be empty or null.", exception.Message);
    }

    [Theory(DisplayName = nameof(Instantiate_InvalidSummary))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Instantiate_InvalidSummary(string? summary)
    {
        Action action = () => new DomainEntity.MarketNews(
            "Test",
            summary!,
            DateTime.Now.AddDays(-2),
            "http://www.teste.com.br",
            "Test Source",
            "http://www.imageurl.com",
            new List<string> { "Author1", "Author2" },
            0.5m,
            "Neutral");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Summary cannot be empty or null.", exception.Message);
    }

    [Theory(DisplayName = nameof(Instantiate_InvalidUrl))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Instantiate_InvalidUrl(string? url)
    {
        Action action = () => new DomainEntity.MarketNews(
            "Test",
            "Test Summary",
            DateTime.Now.AddDays(-2),
            url!,
            "Test Source",
            "http://www.imageurl.com",
            new List<string> { "Author1", "Author2" },
            0.5m,
            "Neutral");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("URL cannot be empty or null.", exception.Message);
    }

    [Theory(DisplayName = nameof(Instantiate_InvalidSource))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Instantiate_InvalidSource(string? source)
    {
        Action action = () => new DomainEntity.MarketNews(
            "Test",
            "Test Summary",
            DateTime.Now.AddDays(-2),
            "http://www.teste.com.br",
            source!,
            "http://www.imageurl.com",
            new List<string> { "Author1", "Author2" },
            0.5m,
            "Neutral");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Source cannot be empty or null.", exception.Message);
    }

    [Theory(DisplayName = nameof(Instantiate_InvalidPublishDate))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [InlineData(1)] 
    public void Instantiate_InvalidPublishDate(int daysInTheFuture)
    {
        var futureDate = DateTime.Now.AddDays(daysInTheFuture);

        Action action = () => new DomainEntity.MarketNews(
            "Test Title",
            "Test Summary",
            futureDate,
            "http://www.teste.com.br",
            "Test Source",
            "http://www.imageurl.com",
            new List<string> { "Author1", "Author2" },
            0.5m,
            "Neutral");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Publish date cannot be in the future.", exception.Message);
    }

    [Theory(DisplayName = nameof(Instantiate_InvalidAuthors))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [MemberData(nameof(GetInvalidAuthors))]
    public void Instantiate_InvalidAuthors(List<string> authors)
    {
        Action action = () => new DomainEntity.MarketNews(
            "Test Title",
            "Test Summary",
            DateTime.Now.AddDays(-2),
            "http://www.teste.com.br",
            "Test Source",
            "http://www.imageurl.com",
            authors,
            0.5m,
            "Neutral");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Authors list cannot be empty.", exception.Message);
    }

    public static IEnumerable<object[]> GetInvalidAuthors()
    {
        yield return new object[] { new List<string>() };
        yield return new object[] { null };
    }



}
