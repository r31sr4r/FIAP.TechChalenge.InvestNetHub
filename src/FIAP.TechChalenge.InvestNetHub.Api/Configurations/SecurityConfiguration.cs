using FIAP.TechChalenge.InvestNetHub.Api.Common.Utilities;

namespace FIAP.TechChalenge.InvestNetHub.Api.Configurations;

public static class SecurityConfiguration
{
    public static IServiceCollection AddSecurityServices(
               this IServiceCollection services
           )
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}
