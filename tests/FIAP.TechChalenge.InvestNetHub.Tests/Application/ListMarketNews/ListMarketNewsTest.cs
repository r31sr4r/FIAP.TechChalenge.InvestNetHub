using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using Moq;

namespace FIAP.TechChalenge.InvestNetHub.Tests.Application.ListMarketNews;

[CollectionDefinition(nameof(ListMarketNewsTestFixture))]
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
        var input = new ListMarketNewsInput(
            page: 2,
            perPage: 15,
            search: "search-example",
            sort: "name",
            dir: SearchOrder.Asc
        );

        var outputRepositorySearch = new OutputSearch<MarketNews>(
                currentPage: input.Page,
                perPage: input.PerPage,
                Items: (IReadOnlyList<MarketNews>)marketNewsList,
                Total: 100
        );

        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.Sort == input.Sort
                && searchInput.Dir == input.Dir                 
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync();

        var useCase = new ListMarketNews(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        output.Items.ForEach(outputItem =>
        {
            var repositoryMarketNews = outputRepositorySearch.Items
                .Find(x => x.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem.Title.Should().Be(repositoryMarketNews.Title);
            outputItem.Summary.Should().Be(repositoryMarketNews.Summary);
            outputItem.PublishDate.Should().Be(repositoryMarketNews.PublishDate);
            outputItem.Url.Should().Be(repositoryMarketNews.Url);
            outputItem.Source.Should().Be(repositoryMarketNews.Source);
            outputItem.ImageUrl.Should().Be(repositoryMarketNews.ImageUrl);
            outputItem.Authors.Should().Be(repositoryMarketNews.Authors);
            outputItem.OverallSentimentScore.Should().Be(repositoryMarketNews.OverallSentimentScore);
            outputItem.OverallSentimentLabel.Should().Be(repositoryMarketNews.OverallSentimentLabel);
        });
        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.Sort == input.Sort
                && searchInput.Dir == input.Dir                 
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);



    }
}
