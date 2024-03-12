using FIAP.TechChalenge.InvestNetHub.Api.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLoggingConfiguration();

builder.Services
    .AddAppConnections(builder.Configuration)
    .AddUseCases()
    .AddRabbitMQ(builder.Configuration)
    .AddMessageProduder()
    .AddMessageConsumer()
    .AddSecurityServices(builder.Configuration)
    .AddAndConfigureControllers();

builder.Logging.AddConsole();

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() => Log.Information("Application started"));
app.Lifetime.ApplicationStopping.Register(() => Log.Information("Application is stopping"));
app.Lifetime.ApplicationStopped.Register(() => Log.Information("Application stopped"));

app.UseDocumentation();
app.MigrateDatabase();
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }