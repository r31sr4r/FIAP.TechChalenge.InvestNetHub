using FIAP.TechChalenge.InvestNetHub.Domain.Events;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Configuration;
internal static class EventsMapping
{
    private static Dictionary<string, string> _routingKeys = new()
    {
        { typeof(UserCreatedEvent).Name, "user.created" }
    };

    public static string GetRoutingKey<T>
        () => _routingKeys[typeof(T).Name];

}
