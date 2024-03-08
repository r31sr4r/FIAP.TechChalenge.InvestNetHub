using FIAP.TechChalenge.InvestNetHub.Application;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using UnitOfWorkInfra = FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.Data.EF.UnitOfWork;

[Collection(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTest
{
    private readonly UnitOfWorkTestFixture _fixture;

    public UnitOfWorkTest(UnitOfWorkTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Commit")]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task Commit()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleUserList = _fixture.GetExampleUserList();
        var userWithEvent = exampleUserList.First();
        var @event = new DomainEventFake();
        userWithEvent.RaiseEvent(@event);
        var eventHandlerMock = new Mock<IDomainEventHandler<DomainEventFake>>();
        await dbContext.Users.AddRangeAsync(exampleUserList);
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddSingleton(eventHandlerMock.Object);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var eventPublisher = new DomainEventPublisher(serviceProvider);
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(
            dbContext,
            eventPublisher,
            serviceProvider.GetRequiredService<ILogger<UnitOfWorkInfra.UnitOfWork>>()
        );

        await unitOfWork.Commit(CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(true);
        var users = await assertDbContext.Users.AsNoTracking().ToListAsync();

        users.Should().HaveCount(exampleUserList.Count);
        eventHandlerMock.Verify(
            handler => handler.HandleAsync(@event, CancellationToken.None),
            Times.Once
        );
        userWithEvent.Events.Should().BeEmpty();
    }

    [Fact(DisplayName = "Rollback")]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task Rollback()
    {
        var dbContext = _fixture.CreateDbContext();
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var eventPublisher = new DomainEventPublisher(serviceProvider);
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(
            dbContext,
            eventPublisher,
            serviceProvider.GetRequiredService<ILogger<UnitOfWorkInfra.UnitOfWork>>()
        );

        var task = async () => await unitOfWork.Rollback(CancellationToken.None);

        await task.Should().NotThrowAsync();
    }
}
