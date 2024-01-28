using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Models;
using FluentAssertions;
using Moq;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.MarketNews.ListMarketNews;

[Collection(nameof(ListMarketNewsTestFixture))]
public class ListMarketNewsTest
{
    private readonly ListMarketNewsTestFixture _fixture;

    public ListMarketNewsTest(ListMarketNewsTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ShouldListMarketNews))]
    [Trait("Application", "ListMarketNews")]
    public async Task ShouldListMarketNews()
    {
        var serviceMock = new Mock<IMarketNewsService>();
        var input = _fixture.GetExampleInput();

        var marketNewsDtoList = _fixture.GetExampleMarketNewsDtoList();
        serviceMock.Setup(x => x.GetMarketNewsAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<DateTime?>(),
            It.IsAny<DateTime?>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(marketNewsDtoList);

        var useCase = new UseCases.ListMarketNews(serviceMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.MarketNews.Should().HaveCount(marketNewsDtoList.Count);

        for (int i = 0; i < marketNewsDtoList.Count; i++)
        {
            var dto = marketNewsDtoList[i];
            var outputItem = output.MarketNews.ElementAt(i);

            outputItem.Title.Should().Be(dto.Title);
            outputItem.Summary.Should().Be(dto.Summary);
            outputItem.PublishDate.Should().Be(dto.PublishDate);
            outputItem.Url.Should().Be(dto.Url);
            outputItem.Source.Should().Be(dto.Source);
            outputItem.ImageUrl.Should().Be(dto.ImageUrl);
            outputItem.Authors.Should().BeEquivalentTo(dto.Authors);
            outputItem.OverallSentimentScore.Should().Be(dto.OverallSentimentScore);
            outputItem.OverallSentimentLabel.Should().Be(dto.OverallSentimentLabel);
        }

        serviceMock.Verify(x => x.GetMarketNewsAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<DateTime?>(),
            It.IsAny<DateTime?>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Fact(DisplayName = nameof(ShouldReturnEmptyListWhenNoData))]
    [Trait("Application", "ListMarketNews")]
    public async Task ShouldReturnEmptyListWhenNoData()
    {
        var serviceMock = new Mock<IMarketNewsService>();
        var input = _fixture.GetExampleInput();

        serviceMock.Setup(x => x.GetMarketNewsAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<DateTime?>(),
            It.IsAny<DateTime?>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(new List<MarketNewsDto>());

        var useCase = new UseCases.ListMarketNews(serviceMock.Object);

        var output = await useCase.Handle(input, cancellationToken: CancellationToken.None);

        output.Should().NotBeNull();
        output.MarketNews.Should().BeEmpty();

        serviceMock.Verify(x => x.GetMarketNewsAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<DateTime?>(),
            It.IsAny<DateTime?>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }


}
