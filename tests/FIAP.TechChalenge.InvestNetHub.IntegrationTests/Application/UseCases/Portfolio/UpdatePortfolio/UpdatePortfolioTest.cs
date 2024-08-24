using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;
using UseCase = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;
using FluentAssertions;
using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.UpdatePortfolio;

[Collection(nameof(UpdatePortfolioTestFixture))]
public class UpdatePortfolioTest
{
    private readonly UpdatePortfolioTestFixture _fixture;

    public UpdatePortfolioTest(UpdatePortfolioTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "UpdatePortfolio")]
    [Trait("Integration/Application", "UpdatePortfolio - Use Cases")]
    public async Task UpdatePortfolio()
    {
        var dbContext = _fixture.CreateDbContext();
        var portfolio = _fixture.GetValidPortfolio();
        
        // Adiciona o portfolio e salva as alterações
        await dbContext.AddAsync(portfolio);
        await dbContext.SaveChangesAsync();
        
        // Cria um novo contexto para evitar o problema de tracking duplicado
        var updatedDbContext = _fixture.CreateDbContext(true);
        var portfolioRepository = new PortfolioRepository(updatedDbContext);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var unitOfWork = new UnitOfWork(
            updatedDbContext,
            null,
            serviceProvider.GetRequiredService<ILogger<UnitOfWork>>()
        );

        var input = new UseCase.UpdatePortfolioInput(
            portfolio.Id,
            _fixture.GetValidPortfolioName(),
            _fixture.GetValidPortfolioDescription()
        );

        var useCase = new UseCase.UpdatePortfolio(
            portfolioRepository,
            unitOfWork
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        var dbPortfolio = await updatedDbContext
            .Portfolios
            .FindAsync(portfolio.Id);

        dbPortfolio.Should().NotBeNull();
        dbPortfolio!.Name.Should().Be(input.Name);
        dbPortfolio.Description.Should().Be(input.Description);

        output.Should().NotBeNull();
        output.Id.Should().Be(portfolio.Id);
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
    }


    [Fact(DisplayName = "ThrowWhenPortfolioNotFound")]
    [Trait("Integration/Application", "UpdatePortfolio - Use Cases")]
    public async Task ThrowWhenPortfolioNotFound()
    {
        var dbContext = _fixture.CreateDbContext();
        var portfolioRepository = new PortfolioRepository(dbContext);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var unitOfWork = new UnitOfWork(
            dbContext,
            null,
            serviceProvider.GetRequiredService<ILogger<UnitOfWork>>()
        );

        var input = new UseCase.UpdatePortfolioInput(
            Guid.NewGuid(),
            _fixture.GetValidPortfolioName(),
            _fixture.GetValidPortfolioDescription()
        );

        var useCase = new UseCase.UpdatePortfolio(
            portfolioRepository,
            unitOfWork
        );

        var task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Portfolio with id {input.Id} not found");
    }
}
