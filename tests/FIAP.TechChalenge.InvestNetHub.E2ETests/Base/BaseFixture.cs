using Bogus;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Base;
public class BaseFixture
{
    protected Faker Faker { get; set; }

    public ApiClient ApiClient { get; set; }

    public CustomWebApplicationFactory<Program> WebAppFactory { get; set; }

    public HttpClient HttpClient { get; set; }

    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
        WebAppFactory = new CustomWebApplicationFactory<Program>();
        HttpClient = WebAppFactory.CreateClient();
        ApiClient = new ApiClient(HttpClient);
    }

    public FiapTechChalengeDbContext CreateDbContext(
        bool preserveData = false
    )
    {
        var context = new FiapTechChalengeDbContext(
            new DbContextOptionsBuilder<FiapTechChalengeDbContext>()
                .UseInMemoryDatabase("e2e-tests-db")
                .Options
        );
        return context;
    }


}
