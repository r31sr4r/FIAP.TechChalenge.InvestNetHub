using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.CreateUser;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.AlphaVantage;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Common;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Interfaces;
using MediatR;

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
}
