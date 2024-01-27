using Bogus;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Base;
public class BaseFixture
{
    protected Faker Faker { get; set; }

    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
    }


}
