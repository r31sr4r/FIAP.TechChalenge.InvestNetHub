using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using Moq;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using UseCase = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.ListPortfolios;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.ListPortfolios;

[Collection(nameof(ListPortfoliosTestFixture))]
public class ListPortfoliosTest
{
    private readonly ListPortfoliosTestFixture _fixture;

    public ListPortfoliosTest(ListPortfoliosTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ShouldReturnPortfolios))]
    [Trait("Application", "ListPortfolios - Use Cases")]
    public async void ShouldReturnPortfolios()
    {
        var portfoliosList = _fixture.GetPortfoliosList();
        var repositoryMock = _fixture.GetRepositoryMock();
        var input = _fixture.GetInput();
        var outputRepositorySearch = new SearchOutput<DomainEntity.Portfolio>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: (IReadOnlyList<DomainEntity.Portfolio>)portfoliosList,
            total: new Random().Next(50, 200)
        );

        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page &&
                               searchInput.PerPage == input.PerPage &&
                               searchInput.Search == input.Search &&
                               searchInput.OrderBy == input.Sort &&
                               searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new UseCase.ListPortfolios(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<PortfolioModelOutput>)output.Items).ForEach(outputItem =>
        {
            var repositoryPortfolio = outputRepositorySearch.Items
                .FirstOrDefault(u => u.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repositoryPortfolio!.Name);
            outputItem.Description.Should().Be(repositoryPortfolio!.Description);
            outputItem.CreatedAt.Should().Be(repositoryPortfolio!.CreatedAt);
            outputItem.Id.Should().Be(repositoryPortfolio!.Id);
        });
        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page &&
                               searchInput.PerPage == input.PerPage &&
                               searchInput.Search == input.Search &&
                               searchInput.OrderBy == input.Sort &&
                               searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Theory(DisplayName = nameof(ListInputWithoutAllParameters))]
    [Trait("Application", "ListPortfolios - Use Cases")]
    [MemberData(nameof(ListPortfoliosTestDataGenerator.GetInputWithoutAllParameters),
        parameters: 18,
        MemberType = typeof(ListPortfoliosTestDataGenerator)
    )]
    public async void ListInputWithoutAllParameters(
        UseCase.ListPortfoliosInput input
    )
    {
        var portfoliosList = _fixture.GetPortfoliosList();
        var repositoryMock = _fixture.GetRepositoryMock();
        var outputRepositorySearch = new SearchOutput<DomainEntity.Portfolio>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: (IReadOnlyList<DomainEntity.Portfolio>)portfoliosList,
            total: new Random().Next(50, 200)
        );

        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page &&
                               searchInput.PerPage == input.PerPage &&
                               searchInput.Search == input.Search &&
                               searchInput.OrderBy == input.Sort &&
                               searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new UseCase.ListPortfolios(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<PortfolioModelOutput>)output.Items).ForEach(outputItem =>
        {
            var repositoryPortfolio = outputRepositorySearch.Items
                .FirstOrDefault(u => u.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repositoryPortfolio!.Name);
            outputItem.Description.Should().Be(repositoryPortfolio!.Description);
            outputItem.CreatedAt.Should().Be(repositoryPortfolio!.CreatedAt);
            outputItem.Id.Should().Be(repositoryPortfolio!.Id);
        });
        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page &&
                               searchInput.PerPage == input.PerPage &&
                               searchInput.Search == input.Search &&
                               searchInput.OrderBy == input.Sort &&
                               searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Fact(DisplayName = nameof(ListOkWhenEmpty))]
    [Trait("Application", "ListPortfolios - Use Cases")]
    public async void ListOkWhenEmpty()
    {
        var portfoliosList = _fixture.GetPortfoliosList();
        var input = _fixture.GetInput();
        var repositoryMock = _fixture.GetRepositoryMock();
        var outputRepositorySearch = new SearchOutput<DomainEntity.Portfolio>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: new List<DomainEntity.Portfolio>().AsReadOnly(),
            total: 0
        );

        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page &&
                               searchInput.PerPage == input.PerPage &&
                               searchInput.Search == input.Search &&
                               searchInput.OrderBy == input.Sort &&
                               searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);

        var useCase = new UseCase.ListPortfolios(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(0);
        output.Items.Should().HaveCount(0);

        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page &&
                               searchInput.PerPage == input.PerPage &&
                               searchInput.Search == input.Search &&
                               searchInput.OrderBy == input.Sort &&
                               searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}
