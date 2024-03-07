namespace FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
public interface IDomainEventPublisher
{
    Task PublishAsync(DomainEvent domainEvent);
}
