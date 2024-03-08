using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
public class UnitOfWork
    : IUnitOfWork
{
    private readonly FiapTechChalengeDbContext _dbContext;
    private readonly IDomainEventPublisher _publisher;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(
        FiapTechChalengeDbContext dbContext,
        IDomainEventPublisher publisher,
        ILogger<UnitOfWork> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        var aggregateRoots = _dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(entry => entry.Entity.Events.Any())
            .Select(entry => entry.Entity);
        
        _logger.LogInformation(
                "Commit: {AggregatesCount} aggregate roots with events.",
                aggregateRoots.Count()
            );


        var events = aggregateRoots
            .SelectMany(entry => entry.Events);

        _logger.LogInformation(
            "Commit: {EventsCount} events to be published.",
            events.Count()
        );


        foreach (var @event in events)
            await _publisher.PublishAsync((dynamic)@event, cancellationToken);

        foreach(var aggregate in aggregateRoots)
            aggregate.ClearEvents();

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task Rollback(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
