using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Base;
public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup>
    where TStartup : class

{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbOptions = services.FirstOrDefault(
                x => x.ServiceType == typeof(
                DbContextOptions<FiapTechChalengeDbContext>
                )
            );

            if(dbOptions != null)
                services.Remove(dbOptions);

            services.AddDbContext<FiapTechChalengeDbContext>(
                options =>
                {
                    options.UseInMemoryDatabase("e2e-tests-db");
                }
            );
        });
    }
}
