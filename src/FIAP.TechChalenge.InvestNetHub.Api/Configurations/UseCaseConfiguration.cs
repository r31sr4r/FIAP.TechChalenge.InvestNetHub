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
               this IServiceCollection services
           )
    {
        services.AddMediatR(typeof(CreateUser));
        services.AddRepositories();
        services.AddExternalServices();
        services.AddDomainEvents();
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
        this IServiceCollection services
       )
    {
        services.AddTransient<IDomainEventPublisher, DomainEventPublisher>();
        services.AddTransient<IDomainEventHandler<UserCreatedEvent>, SendToAnalysisEventHandler>();

        return services;
    }
}
