using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChalenge.InvestNetHub.Api.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConnections(
        this IServiceCollection services)
    {
        services.AddDbConnection();
        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services)
    {
        services.AddDbContext<FiapTechChalengeDbContext>(
            options => options.UseInMemoryDatabase(
                "FiapTechChalengeDb")
        );

        return services;
    }
}
