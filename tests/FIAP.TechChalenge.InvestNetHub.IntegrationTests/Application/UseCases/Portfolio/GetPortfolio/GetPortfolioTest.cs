using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
using FluentAssertions;
using UseCase = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.GetPortfolio;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.GetPortfolio;

[Collection(nameof(GetPortfolioTestFixture))]
public class GetPortfolioTest
{
    private readonly GetPortfolioTestFixture _fixture;

    public GetPortfolioTest(GetPortfolioTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(GetPortfolio))]
    [Trait("Integration/Application", "GetPortfolio - Use Cases")]
    public async Task GetPortfolio()
    {
        var dbContext = _fixture.CreateDbContext();
        var examplePortfolio = _fixture.GetValidPortfolio();
        dbContext.Add(examplePortfolio);
        dbContext.SaveChanges();
        var portfolioRepository = new PortfolioRepository(dbContext);

        var input = new UseCase.GetPortfolioInput(examplePortfolio.Id);
        var useCase = new UseCase.GetPortfolio(portfolioRepository);

        var output = await useCase.Handle(input, CancellationToken.None);

        var dbPortfolio = await (_fixture.CreateDbContext(true))
            .Portfolios
            .FindAsync(examplePortfolio.Id);

        dbPortfolio.Should().NotBeNull();
        dbPortfolio!.Name.Should().Be(examplePortfolio.Name);
        dbPortfolio.Description.Should().Be(examplePortfolio.Description);
        dbPortfolio.Id.Should().Be(examplePortfolio.Id);

        output.Should().NotBeNull();
        output!.Name.Should().Be(examplePortfolio.Name);
        output.Description.Should().Be(examplePortfolio.Description);
        output.Id.Should().Be(examplePortfolio.Id);
    }

    [Fact(DisplayName = nameof(NotFoundExceptionWhenPortfolioDoesntExist))]
    [Trait("Integration/Application", "GetPortfolio - Use Cases")]
    public async Task NotFoundExceptionWhenPortfolioDoesntExist()
    {
        var dbContext = _fixture.CreateDbContext();
        var examplePortfolio = _fixture.GetValidPortfolio();
        dbContext.Add(examplePortfolio);
        dbContext.SaveChanges();
        var portfolioRepository = new PortfolioRepository(dbContext);
        var input = new UseCase.GetPortfolioInput(Guid.NewGuid());
        var useCase = new UseCase.GetPortfolio(portfolioRepository);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None
        );

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Portfolio with id {input.Id} not found");
    }
}
