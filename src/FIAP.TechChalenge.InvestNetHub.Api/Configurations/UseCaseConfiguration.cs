using FIAP.TechChalenge.InvestNetHub.Application;
using FIAP.TechChalenge.InvestNetHub.Application.EventHandlers;
using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.CreateUser;
using FIAP.TechChalenge.InvestNetHub.Domain.Events;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.AlphaVantage;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Common;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Configuration;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Producer;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace FIAP.TechChalenge.InvestNetHub.Api.Configurations;

public static class UseCaseConfiguration
{
    public static IServiceCollection AddUseCases(
               this IServiceCollection services,
               IConfiguration configuration
           )
    {
        services.AddMediatR(typeof(CreateUser));
        services.AddRepositories();
        services.AddExternalServices();
        services.AddDomainEvents(configuration);
        services.AddTransient<IMarketNewsRepository, MarketNewsRepository>();

        return services;
    }

    private static IServiceCollection AddRepositories(
           this IServiceCollection services
       )
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }

    private static IServiceCollection AddExternalServices(
        this IServiceCollection services
       )
    {
        services.AddTransient<IMarketNewsService, AlphaVantageMarketNewsService>();
        services.AddTransient<IMarketNewsMapper, MarketNewsMapper>();

        return services;
    }

    private static IServiceCollection AddDomainEvents(
        this IServiceCollection services,
        IConfiguration configuration
       )
    {
        services.AddTransient<IDomainEventPublisher, DomainEventPublisher>();
        services.AddTransient<IDomainEventHandler<UserCreatedEvent>, SendToAnalysisEventHandler>();

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

        services.AddTransient<IMessageProducer>(sp =>
        {
            var channelManager = sp.GetRequiredService<ChannelManager>();
            var config = sp.GetRequiredService<IOptions<RabbitMQConfiguration>>();
            return new RabbitMQProducer(channelManager.GetChannel(), config);
        });

        return services;
    }
}
