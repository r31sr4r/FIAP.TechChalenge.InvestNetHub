using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Base;
public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup>
    where TStartup : class

{
    public IModel RabbitMQChannel { get; private set; }
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
                var context = scope.ServiceProvider
                    .GetService<FiapTechChalengeDbContext>();
                ArgumentNullException.ThrowIfNull(context);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        });

        base.ConfigureWebHost(builder);
    }
}
