namespace FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
public interface IMessageProducer
{
    Task SendMessageAsync<T>(T message, CancellationToken cancellationToken);
}
