using FIAP.TechChalenge.InvestNetHub.Api.Configurations.Policies;
using FIAP.TechChalenge.InvestNetHub.Api.Filters;

namespace FIAP.TechChalenge.InvestNetHub.Api.Configurations;

public static class ControllersConfiguration
{
    public static IServiceCollection AddAndConfigureControllers(
        this IServiceCollection services
    )
    {
        services
            .AddControllers(
            options => options.Filters.Add(typeof(ApiGlobalExceptionFilter))
            )   
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCasePolicy();
            });
        services.AddDocumentation();
        return services;
    }

    private static IServiceCollection AddDocumentation(
        this IServiceCollection services
    )
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static WebApplication UseDocumentation(
        this WebApplication app
    )
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }

}
