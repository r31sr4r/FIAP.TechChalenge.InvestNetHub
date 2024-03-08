using RabbitMQ.Client;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Configuration;
public class ChannelManager
{
    private readonly IConnection _connection;
    private readonly object _lock = new();
    private IModel? _channel;

    public ChannelManager(IConnection connection)
    {
        _connection = connection;
    }

    public IModel GetChannel()
    {
        lock (_lock)
        {
            if (_channel is null || _channel.IsClosed)
            {
                _channel = _connection.CreateModel();
                _channel.ConfirmSelect();
            }

            return _channel;
        }
    }
}
