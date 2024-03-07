using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.Events;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;

namespace FIAP.TechChalenge.InvestNetHub.Application.EventHandlers;
public class SendToAnalysisEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    private readonly IMessageProducer _messageProducer;

    public SendToAnalysisEventHandler(IMessageProducer messageProducer) 
        => _messageProducer = messageProducer;

    public Task HandleAsync(UserCreatedEvent domainEvent, CancellationToken cancellationToken)
        => _messageProducer.SendMessageAsync(domainEvent, cancellationToken);
 
}
