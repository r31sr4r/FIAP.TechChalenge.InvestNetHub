using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.ListPortfolios;
using UseCase = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.ListPortfolios;
using FluentAssertions;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.ListPortfolio;

[Collection(nameof(ListPortfoliosTestFixture))]
public class ListPortfoliosTest
{
    private readonly ListPortfoliosTestFixture _fixture;

    public ListPortfoliosTest(ListPortfoliosTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "SearchReturnsListAndTotal")]
    [Trait("Integration/Application", "ListPortfolios - Use Cases")]
    public async Task SearchReturnsListAndTotal()
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var examplePortfolioList = _fixture.GetPortfoliosList(15);
        await dbContext.AddRangeAsync(examplePortfolioList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var portfolioRepository = new PortfolioRepository(dbContext);
        var searchInput = new ListPortfoliosInput(page: 1, perPage: 10);
        var useCase = new UseCase.ListPortfolios(portfolioRepository);

        var output = await useCase.Handle(searchInput, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(searchInput.Page);
        output.PerPage.Should().Be(searchInput.PerPage);
        output.Total.Should().Be(examplePortfolioList.Count);
        output.Items.Should().HaveCount(10);
        foreach (PortfolioModelOutput outputItem in output.Items)
        {
            var exampleItem = examplePortfolioList.Find(x => x.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem!.Id.Should().Be(exampleItem!.Id);
            outputItem.Name.Should().Be(exampleItem.Name);
            outputItem.Description.Should().Be(exampleItem.Description);
        }
    }

    [Fact(DisplayName = "SearchReturnsEmpty")]
    [Trait("Integration/Application", "ListPortfolios - Use Cases")]
    public async Task SearchReturnsEmpty()
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var portfolioRepository = new PortfolioRepository(dbContext);
        var searchInput = new ListPortfoliosInput(page: 1, perPage: 10);
        var useCase = new UseCase.ListPortfolios(portfolioRepository);

        var output = await useCase.Handle(searchInput, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(searchInput.Page);
        output.PerPage.Should().Be(searchInput.PerPage);
        output.Total.Should().Be(0);
        output.Items.Should().HaveCount(0);
    }

    [Theory(DisplayName = "SearchReturnsPaginated")]
    [Trait("Integration/Application", "ListPortfolios - Use Cases")]
    [InlineData(10, 1, 5, 5)]
    [InlineData(7, 2, 5, 2)]
    [InlineData(10, 2, 5, 5)]
    [InlineData(7, 3, 5, 0)]
    public async Task SearchReturnsPaginated(
        int itemsToGenerate,
        int page,
        int perPage,
        int expectedTotal
    )
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var examplePortfolioList = _fixture.GetPortfoliosList(itemsToGenerate);
        await dbContext.AddRangeAsync(examplePortfolioList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var portfolioRepository = new PortfolioRepository(dbContext);
        var searchInput = new ListPortfoliosInput(page, perPage);
        var useCase = new UseCase.ListPortfolios(portfolioRepository);

        var output = await useCase.Handle(searchInput, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(searchInput.Page);
        output.PerPage.Should().Be(searchInput.PerPage);
        output.Total.Should().Be(examplePortfolioList.Count);
        output.Items.Should().HaveCount(expectedTotal);
        foreach (PortfolioModelOutput outputItem in output.Items)
        {
            var exampleItem = examplePortfolioList.Find(x => x.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem!.Id.Should().Be(exampleItem!.Id);
            outputItem.Name.Should().Be(exampleItem.Name);
            outputItem.Description.Should().Be(exampleItem.Description);
        }
    }

    [Theory(DisplayName = "SearchOrdered")]
    [Trait("Integration/Application", "ListPortfolios - Use Cases")]
    [InlineData("name", "asc")]
    [InlineData("name", "desc")]
    [InlineData("createdAt", "asc")]
    [InlineData("createdAt", "desc")]
    public async Task SearchOrdered(
        string orderBy,
        string order
    )
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var examplePortfolioList = _fixture.GetPortfoliosList(10);
        await dbContext.AddRangeAsync(examplePortfolioList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var portfolioRepository = new PortfolioRepository(dbContext);
        var searchOrder = order == "asc" ? SearchOrder.Asc : SearchOrder.Desc;
        var searchInput = new ListPortfoliosInput(
            page: 1,
            perPage: 20,
            "",
            orderBy,
            searchOrder
        );
        var useCase = new UseCase.ListPortfolios(portfolioRepository);

        var output = await useCase.Handle(searchInput, CancellationToken.None);

        var expectOrdered = _fixture.SortList(examplePortfolioList, orderBy, searchOrder);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(searchInput.Page);
        output.PerPage.Should().Be(searchInput.PerPage);
        output.Total.Should().Be(examplePortfolioList.Count);
        output.Items.Should().HaveCount(examplePortfolioList.Count);

        for (int i = 0; i < output.Items.Count; i++)
        {
            var outputItem = output.Items[i];
            var exampleItem = expectOrdered[i];
            outputItem.Should().NotBeNull();
            outputItem!.Id.Should().Be(exampleItem.Id);
            outputItem.Name.Should().Be(exampleItem.Name);
            outputItem.Description.Should().Be(exampleItem.Description);
        }
    }
}
