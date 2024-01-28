using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FIAP.TechChalenge.InvestNetHub.Api.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConnections(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbConnection(configuration);
        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("InvestHubDb");
        services.AddDbContext<FiapTechChalengeDbContext>(
            options => options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
        );

        return services;
    }
}
