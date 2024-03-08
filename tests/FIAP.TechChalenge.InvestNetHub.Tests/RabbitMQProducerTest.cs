using FIAP.TechChalenge.InvestNetHub.Domain.Events;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Configuration;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Producer;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests;
public class RabbitMQProducerTest
{
    [Fact]
    public async Task ShouldProduceMessage()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ConfirmSelect();
        var options = Options.Create(new RabbitMQConfiguration
        {
            Exchange = "user.events"
        });
        var producer = new RabbitMQProducer(channel, options);

        var @event = new UserCreatedEvent(
            Guid.NewGuid(),
            "12345678900"            
        );

        await producer.SendMessageAsync(@event, CancellationToken.None);

    }
}
