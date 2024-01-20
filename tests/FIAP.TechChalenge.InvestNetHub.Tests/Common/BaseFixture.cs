using Bogus;

namespace FIAP.TechChalenge.InvestNetHub.Tests.Common;
public abstract class BaseFixture
{
    protected BaseFixture()
        => Faker = new Faker("pt_BR");

    public Faker Faker { get; set; }
}
