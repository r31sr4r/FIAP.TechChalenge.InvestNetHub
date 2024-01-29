using FIAP.TechChalenge.InvestNetHub.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAppConnections(builder.Configuration)
    .AddUseCases()
    .AddSecurityServices(builder.Configuration)
    .AddAndConfigureControllers();

builder.Logging.AddConsole();

var app = builder.Build();
app.UseDocumentation();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }