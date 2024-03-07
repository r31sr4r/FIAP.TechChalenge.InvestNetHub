using FluentAssertions;
using static Xunit.Assert;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Domain.SeedWork;
public class AggregateRootTest
{
    [Fact(DisplayName = nameof(RaiseEvent))]
    [Trait("Domain", "User - AggregateRoot")]
    public void RaiseEvent()
    {
        var domainEvent = new DomainEventFake();
        var aggregateRoot = new AggregateRootFake();

        aggregateRoot.RaiseEvent(domainEvent);

        aggregateRoot.Events.Should().HaveCount(1);
    }

    [Fact(DisplayName = nameof(ClearEvents))]
    [Trait("Domain", "User - AggregateRoot")]
    public void ClearEvents()
    {
        var domainEvent = new DomainEventFake();
        var aggregateRoot = new AggregateRootFake();
        aggregateRoot.RaiseEvent(domainEvent);

        aggregateRoot.ClearEvents();

        aggregateRoot.Events.Should().BeEmpty();
    }
}
