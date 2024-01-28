using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Base;
public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup>
    where TStartup : class

{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("E2ETest");
        builder.ConfigureServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider
                    .GetService<FiapTechChalengeDbContext>();
                ArgumentNullException.ThrowIfNull(context);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        });

        base.ConfigureWebHost(builder);
    }
}
