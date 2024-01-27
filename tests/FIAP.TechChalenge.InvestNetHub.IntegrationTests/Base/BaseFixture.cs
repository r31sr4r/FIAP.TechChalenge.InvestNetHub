using Bogus;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Base;
public class BaseFixture
{
    protected Faker Faker { get; set; }

    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
    }

    public FiapTechChalengeDbContext CreateDbContext(
        bool preserveData = false
    )
    {
        var context = new FiapTechChalengeDbContext(
            new DbContextOptionsBuilder<FiapTechChalengeDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
        );

        if (!preserveData)
            context.Database.EnsureDeleted();

        return context;

    }


}
