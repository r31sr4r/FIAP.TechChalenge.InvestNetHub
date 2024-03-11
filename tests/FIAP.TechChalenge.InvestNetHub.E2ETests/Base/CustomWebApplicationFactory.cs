using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Base;
public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup>, IDisposable
    where TStartup : class

{
    private const string UserCreatedRoutingKey = "user.created";
    private const string UserAnalysisResultRoutingKey = "user.created";
    public IModel RabbitMQChannel { get; private set; }
    public string UserCreatedQueue => "user.created.queue";
    public RabbitMQConfiguration RabbitMQConfiguration { get; private set; }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var environment = "E2ETest";
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", environment);
        builder.UseEnvironment(environment);
        builder.ConfigureServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                RabbitMQChannel = scope.ServiceProvider
                    .GetService<ChannelManager>()!
                    .GetChannel();
                RabbitMQConfiguration = scope.ServiceProvider
                    .GetService<IOptions<RabbitMQConfiguration>>()!
                    .Value;
                SetupRabbitMQ();
                var context = scope.ServiceProvider
                    .GetService<FiapTechChalengeDbContext>();
                ArgumentNullException.ThrowIfNull(context);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        });

        base.ConfigureWebHost(builder);
    }

    public void SetupRabbitMQ()
    {
        var channel = RabbitMQChannel;
        var exchange = RabbitMQConfiguration.Exchange;
        channel.ExchangeDeclare(exchange, "direct", true, true, null);
        channel.QueueDeclare(UserCreatedQueue, true, false, false, null);
        channel.QueueBind(UserCreatedQueue, exchange, UserCreatedRoutingKey, null);
        channel.QueueDeclare(RabbitMQConfiguration.UserAnalysisResultQueue, true, false, false, null);
        channel.QueueBind(RabbitMQConfiguration.UserAnalysisResultQueue, exchange, UserAnalysisResultRoutingKey, null);
    }

    public override ValueTask DisposeAsync()
    {
        TearDownRabbitMQ();
        return base.DisposeAsync();
    }

    private void TearDownRabbitMQ()
    {
        var channel = RabbitMQChannel;
        var exchange = RabbitMQConfiguration.Exchange;
        channel.QueueUnbind(UserCreatedQueue, exchange, UserCreatedRoutingKey, null);
        channel.QueueDelete(UserCreatedQueue, false, false);
        channel.QueueUnbind(RabbitMQConfiguration.UserAnalysisResultQueue, exchange, UserAnalysisResultRoutingKey, null);
        channel.QueueDelete(RabbitMQConfiguration.UserAnalysisResultQueue, false, false);
        channel.ExchangeDelete(UserCreatedQueue, false);
    }

}
