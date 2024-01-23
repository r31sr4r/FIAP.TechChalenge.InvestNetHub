using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Domain.Entity.MarketNews;

[Collection(nameof(MarketNewsTestFixture))]
public class MarketNewsTest
{
    private readonly MarketNewsTestFixture _marketNewsTestFixturee;

    public MarketNewsTest(MarketNewsTestFixture marketNewsTestFixturee)
        => _marketNewsTestFixturee = marketNewsTestFixturee;

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "MarketNews - Aggregates")]
    public void Instantiate()
    {
        var validMarketNews = _marketNewsTestFixturee.GetValidMarketNews();

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

        marketNews.Should().NotBeNull();
        marketNews.Should().BeOfType<DomainEntity.MarketNews>();
        marketNews.Id.Should().NotBeEmpty();
        marketNews.Title.Should().Be(validMarketNews.Title);
        marketNews.Summary.Should().Be(validMarketNews.Summary);
        marketNews.PublishDate.Should().Be(validMarketNews.PublishDate);
        marketNews.Url.Should().Be(validMarketNews.Url);
        marketNews.Source.Should().Be(validMarketNews.Source);
        marketNews.ImageUrl.Should().Be(validMarketNews.ImageUrl);
        marketNews.Authors.Should().BeEquivalentTo(validMarketNews.Authors);
        marketNews.OverallSentimentScore.Should().Be(validMarketNews.OverallSentimentScore);
        marketNews.OverallSentimentLabel.Should().Be(validMarketNews.OverallSentimentLabel);

    }

    [Theory(DisplayName = nameof(Instantiate_InvalidTitle))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Instantiate_InvalidTitle(string? title)
    {
        var validMarketNews = _marketNewsTestFixturee.GetValidMarketNews();

        Action action = () => new DomainEntity.MarketNews(
            title!,
            validMarketNews.Summary,
            validMarketNews.PublishDate,
            validMarketNews.Url,
            validMarketNews.Source,
            validMarketNews.ImageUrl,
            validMarketNews.Authors,
            validMarketNews.OverallSentimentScore,
            validMarketNews.OverallSentimentLabel
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Title cannot be empty or null.");
    }

    [Theory(DisplayName = nameof(Instantiate_InvalidSummary))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Instantiate_InvalidSummary(string? summary)
    {
        var validMarketNews = _marketNewsTestFixturee.GetValidMarketNews();

        Action action = () => new DomainEntity.MarketNews(
            validMarketNews.Title,
            summary!,
            validMarketNews.PublishDate,
            validMarketNews.Url,
            validMarketNews.Source,
            validMarketNews.ImageUrl,
            validMarketNews.Authors,
            validMarketNews.OverallSentimentScore,
            validMarketNews.OverallSentimentLabel
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Summary cannot be empty or null.");
    }

    [Theory(DisplayName = nameof(Instantiate_InvalidUrl))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Instantiate_InvalidUrl(string? url)
    {
        var validMarketNews = _marketNewsTestFixturee.GetValidMarketNews();

        Action action = () => new DomainEntity.MarketNews(
            validMarketNews.Title,
            validMarketNews.Summary,
            validMarketNews.PublishDate,
            url!,
            validMarketNews.Source,
            validMarketNews.ImageUrl,
            validMarketNews.Authors,
            validMarketNews.OverallSentimentScore,
            validMarketNews.OverallSentimentLabel
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("URL cannot be empty or null.");
    }

    [Theory(DisplayName = nameof(Instantiate_InvalidSource))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Instantiate_InvalidSource(string? source)
    {
        var validMarketNews = _marketNewsTestFixturee.GetValidMarketNews();

        Action action = () => new DomainEntity.MarketNews(
            validMarketNews.Title,
            validMarketNews.Summary,
            validMarketNews.PublishDate,
            validMarketNews.Url,
            source!,
            validMarketNews.ImageUrl,
            validMarketNews.Authors,
            validMarketNews.OverallSentimentScore,
            validMarketNews.OverallSentimentLabel
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Source cannot be empty or null.");
    }

    [Theory(DisplayName = nameof(Instantiate_InvalidPublishDate))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [InlineData(1)]
    public void Instantiate_InvalidPublishDate(int daysInTheFuture)
    {
        var validMarketNews = _marketNewsTestFixturee.GetValidMarketNews();
        var futureDate = DateTime.Now.AddDays(daysInTheFuture);

        Action action = () => new DomainEntity.MarketNews(
            validMarketNews.Title,
            validMarketNews.Summary,
            futureDate,
            validMarketNews.Url,
            validMarketNews.Source,
            validMarketNews.ImageUrl,
            validMarketNews.Authors,
            validMarketNews.OverallSentimentScore,
            validMarketNews.OverallSentimentLabel
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("PublishDate date cannot be in the future.");
    }

    [Theory(DisplayName = nameof(Instantiate_InvalidAuthors))]
    [Trait("Domain", "MarketNews - Aggregates")]
    [MemberData(nameof(GetInvalidAuthors))]
    public void Instantiate_InvalidAuthors(List<string> authors)
    {
        var validMarketNews = _marketNewsTestFixturee.GetValidMarketNews();

        Action action = () => new DomainEntity.MarketNews(
            validMarketNews.Title,
            validMarketNews.Summary,
            validMarketNews.PublishDate,
            validMarketNews.Url,
            validMarketNews.Source,
            validMarketNews.ImageUrl,
            authors,
            validMarketNews.OverallSentimentScore,
            validMarketNews.OverallSentimentLabel
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Authors list cannot be empty.");
    }

    public static IEnumerable<object[]> GetInvalidAuthors()
    {
        yield return new object[] { new List<string>() };
        yield return new object[] { null! };
    }



}
