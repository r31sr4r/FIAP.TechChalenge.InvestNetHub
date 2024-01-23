using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using Moq;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.Common;

namespace FIAP.TechChalenge.InvestNetHub.Tests.Application.ListMarketNews;

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
        var marketNewsList = _fixture.GetExampleMarketNewsList();
        var repositoryMock = _fixture.GetRepositoryMock();
        var input = _fixture.GetExampleInput();

        var outputRepositorySearch = new SearchOutput<MarketNews>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: (IReadOnlyList<MarketNews>)marketNewsList,
                total: (new Random()).Next(50, 200)
        );

        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir                 
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new UseCases.ListMarketNews(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<MarketNewsModelOutput>)output.Items).ForEach(outputItem =>
        {
            var repositoryMarketNews = outputRepositorySearch.Items
                .FirstOrDefault(x => x.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem.Title.Should().Be(repositoryMarketNews!.Title);
            outputItem.Summary.Should().Be(repositoryMarketNews.Summary);
            outputItem.PublishDate.Should().Be(repositoryMarketNews.PublishDate);
            outputItem.Url.Should().Be(repositoryMarketNews.Url);
            outputItem.Source.Should().Be(repositoryMarketNews.Source);
            outputItem.ImageUrl.Should().Be(repositoryMarketNews.ImageUrl);
            outputItem.Authors.ForEach(author =>
            {
                repositoryMarketNews.Authors.Should().Contain(author);
            });
            outputItem.OverallSentimentScore.Should().Be(repositoryMarketNews.OverallSentimentScore);
            outputItem.OverallSentimentLabel.Should().Be(repositoryMarketNews.OverallSentimentLabel);
        });
        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Theory(DisplayName = nameof(ShouldListMarketNewsWithoutAllParameters))]
    [Trait("Application", "ListMarketNews")]
    [MemberData(
        nameof(ListMarketNewsTestDataGenerator.GetInputWithoutAllParameters),
        parameters: 15,
        MemberType = typeof(ListMarketNewsTestDataGenerator)
    )]
    public async Task ShouldListMarketNewsWithoutAllParameters(UseCases.ListMarketNewsInput input)
    {
        var marketNewsList = _fixture.GetExampleMarketNewsList();
        var repositoryMock = _fixture.GetRepositoryMock();

        var outputRepositorySearch = new SearchOutput<MarketNews>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: (IReadOnlyList<MarketNews>)marketNewsList,
                total: (new Random()).Next(50, 200)
        );

        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new UseCases.ListMarketNews(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<MarketNewsModelOutput>)output.Items).ForEach(outputItem =>
        {
            var repositoryMarketNews = outputRepositorySearch.Items
                .FirstOrDefault(x => x.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem.Title.Should().Be(repositoryMarketNews!.Title);
            outputItem.Summary.Should().Be(repositoryMarketNews.Summary);
            outputItem.PublishDate.Should().Be(repositoryMarketNews.PublishDate);
            outputItem.Url.Should().Be(repositoryMarketNews.Url);
            outputItem.Source.Should().Be(repositoryMarketNews.Source);
            outputItem.ImageUrl.Should().Be(repositoryMarketNews.ImageUrl);
            outputItem.Authors.ForEach(author =>
            {
                repositoryMarketNews.Authors.Should().Contain(author);
            });
            outputItem.OverallSentimentScore.Should().Be(repositoryMarketNews.OverallSentimentScore);
            outputItem.OverallSentimentLabel.Should().Be(repositoryMarketNews.OverallSentimentLabel);
        });
        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Fact(DisplayName = nameof(ShouldRetunsEmptyList))]
    [Trait("Application", "ListMarketNews")]
    public async Task ShouldRetunsEmptyList()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var input = _fixture.GetExampleInput();

        var outputRepositorySearch = new SearchOutput<MarketNews>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: (new List<MarketNews>()).AsReadOnly(),
                total: 0
        );

        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new UseCases.ListMarketNews(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(0);
        output.Items.Should().HaveCount(0);

        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}
