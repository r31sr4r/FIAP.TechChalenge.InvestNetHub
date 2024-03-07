using FIAP.TechChalenge.InvestNetHub.Application.EventHandlers;
using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.Events;
using Moq;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.EventHandlers;
public class SendToAnalysisEventHandlerTest
{
    [Fact(DisplayName = nameof(HandleAsync))]
    [Trait("Application", "EventHandlers")]
    public async Task HandleAsync()
    {
        var messageProducerMock = new Mock<IMessageProducer>();
        messageProducerMock.Setup(x => x.SendMessageAsync(
                It.IsAny<UserCreatedEvent>(), 
                It.IsAny<CancellationToken>()
            ))
            .Returns(Task.CompletedTask);
        var handler = new SendToAnalysisEventHandler(messageProducerMock.Object);
        UserCreatedEvent @event = new(Guid.NewGuid(), "12345678901");

        await handler.HandleAsync(@event, CancellationToken.None);

        messageProducerMock.Verify(x => x.SendMessageAsync(@event, CancellationToken.None),
            Times.Once);
    }
}
