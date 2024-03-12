using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Configuration;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Consumer;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Producer;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace FIAP.TechChalenge.InvestNetHub.Api.Configurations;

public static class MessagingConfiguration
{
    public static IServiceCollection AddRabbitMQ(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<RabbitMQConfiguration>(
            configuration.GetSection(RabbitMQConfiguration.ConfigurationSection)
        );
        services.AddSingleton(sp =>
        {
            RabbitMQConfiguration config = sp
                .GetRequiredService<IOptions<RabbitMQConfiguration>>().Value;
            var factory = new ConnectionFactory
            {
                HostName = config.HostName,
                Port = config.Port,
                UserName = config.UserName,
                Password = config.Password
            };
            return factory.CreateConnection();
        });

        services.AddSingleton<ChannelManager>();

        return services;
    }

    public static IServiceCollection AddMessageProduder(
        this IServiceCollection services
    )
    {
        services.AddTransient<IMessageProducer>(sp =>
        {
            var channelManager = sp.GetRequiredService<ChannelManager>();
            var config = sp.GetRequiredService<IOptions<RabbitMQConfiguration>>();
            return new RabbitMQProducer(channelManager.GetChannel(), config);
        });

        return services;
    }

    public static IServiceCollection AddMessageConsumer(
        this IServiceCollection services
    )
    {
        services.AddHostedService(
            sp =>
            {
                var config = sp.GetRequiredService<IOptions<RabbitMQConfiguration>>();
                var connection = sp.GetRequiredService<IConnection>();
                var logger = sp.GetRequiredService<ILogger<UserAnalysisResultEventConsumer>>();
                var channel = connection.CreateModel();
                return new UserAnalysisResultEventConsumer(
                    sp, 
                    logger, 
                    config, 
                    channel
                );
            }
        );

        return services;
    }
}
