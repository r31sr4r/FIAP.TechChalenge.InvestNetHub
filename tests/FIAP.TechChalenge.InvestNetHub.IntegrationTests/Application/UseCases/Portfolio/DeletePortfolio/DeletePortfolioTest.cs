using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;
using UseCase = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeletePortfolio;
using FluentAssertions;
using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.DeletePortfolio;

[Collection(nameof(DeletePortfolioTestFixture))]
public class DeletePortfolioTest
{
    private readonly DeletePortfolioTestFixture _fixture;

    public DeletePortfolioTest(DeletePortfolioTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(DeletePortfolio))]
    [Trait("Integration/Application", "DeletePortfolio - Use Cases")]
    public async Task DeletePortfolio()
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new PortfolioRepository(dbContext);
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var eventPublisher = new DomainEventPublisher(serviceProvider);
        var unitOfWork = new UnitOfWork(
            dbContext,
            eventPublisher,
            serviceProvider.GetRequiredService<ILogger<UnitOfWork>>()
        );
        var portfolioExample = _fixture.GetValidPortfolio();
        await dbContext.AddRangeAsync(_fixture.GetPortfoliosList());
        var tracking = await dbContext.AddAsync(portfolioExample);
        dbContext.SaveChanges();
        tracking.State = EntityState.Detached;
        var useCase = new UseCase.DeletePortfolio(repository, unitOfWork);
        var input = new UseCase.DeletePortfolioInput(portfolioExample.Id);

        await useCase.Handle(input, CancellationToken.None);

        var dbPortfolio = await (_fixture.CreateDbContext(true))
            .Portfolios
            .FindAsync(portfolioExample.Id);

        dbPortfolio.Should().BeNull();
    }

    [Fact(DisplayName = nameof(ThrowWhenPortfolioNotFound))]
    [Trait("Integration/Application", "DeletePortfolio - Use Cases")]
    public async Task ThrowWhenPortfolioNotFound()
    {
        var dbContext = _fixture.CreateDbContext();
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var eventPublisher = new DomainEventPublisher(serviceProvider);
        var unitOfWork = new UnitOfWork(
            dbContext,
            eventPublisher,
            serviceProvider.GetRequiredService<ILogger<UnitOfWork>>()
        );
        var portfolioRepository = new PortfolioRepository(dbContext);
        var input = new UseCase.DeletePortfolioInput(Guid.NewGuid());
        var useCase = new UseCase.DeletePortfolio(portfolioRepository, unitOfWork);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None
        );

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Portfolio with id {input.Id} not found");
    }
}
