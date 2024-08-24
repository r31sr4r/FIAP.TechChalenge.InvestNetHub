using FIAP.TechChalenge.InvestNetHub.Application;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UseCase = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.Portfolio.CreatePortfolio;

[Collection(nameof(CreatePortfolioTestFixture))]
public class CreatePortfolioTest
{
    private readonly CreatePortfolioTestFixture _fixture;

    public CreatePortfolioTest(CreatePortfolioTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreatePortfolio))]
    [Trait("Integration/Application", "Create Portfolio - Use Cases")]
    public async Task CreatePortfolio()
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

        var useCase = new UseCase.CreatePortfolio(
            repository, unitOfWork
        );

        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        var dbPortfolio = await (_fixture.CreateDbContext(true))
            .Portfolios.FindAsync(output.Id);

        dbPortfolio.Should().NotBeNull();
        dbPortfolio!.Name.Should().Be(input.Name);
        dbPortfolio.Description.Should().Be(input.Description);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantiatePortfolio))]
    [Trait("Integration/Application", "Create Portfolio - Use Cases")]
    [MemberData(
        nameof(CreatePortfolioTestDataGenerator.GetInvalidInputs),
        parameters: 6,
        MemberType = typeof(CreatePortfolioTestDataGenerator)
        )]
    public async Task ThrowWhenCantInstantiatePortfolio(
        CreatePortfolioInput input,
        string expectionMessage
    )
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

        var useCase = new UseCase.CreatePortfolio(
            repository, unitOfWork
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(expectionMessage);

        var dbPortfolio = _fixture.CreateDbContext(true)
            .Portfolios.AsNoTracking()
            .ToList();

        dbPortfolio.Should().HaveCount(0);
    }
}




