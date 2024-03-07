namespace FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
public interface IDomainEventPublisher
{
    Task PublishAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken)
        where TDomainEvent : DomainEvent;
}
