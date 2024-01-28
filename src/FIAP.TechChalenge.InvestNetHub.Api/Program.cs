using FIAP.TechChalenge.InvestNetHub.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAppConnections(builder.Configuration)
    .AddUseCases()
    .AddSecurityServices()
    .AddAndConfigureControllers();

builder.Logging.AddConsole();

var app = builder.Build();
app.UseDocumentation();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }