using Bogus.Extensions.Brazil;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Base;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.Data.EF.Repositories.UserRepository;


[CollectionDefinition(nameof(UserRepositoryTestFixture))]
public class UserRepositoryTestFixtureCollection
    : ICollectionFixture<UserRepositoryTestFixture>
{ }

public class UserRepositoryTestFixture
    : BaseFixture
{
    public string GetValidUserName()
    {
        var userName = "";
        while (userName.Length < 3)
            userName = Faker.Person.FullName;
        if (userName.Length > 255)
            userName = userName[..255];
        return userName;
    }

    public string GetValidEmail()
        => Faker.Internet.Email();

    public string GetValidPhone()
    {
        var phoneNumber = Faker.Random.Bool()
            ? Faker.Phone.PhoneNumber("(##) ####-####")
            : Faker.Phone.PhoneNumber("(##) #####-####");

        return phoneNumber;
    }

    public string GetValidCPF()
        => Faker.Person.Cpf();

    public string GetValidRG()
        => Faker.Person.Random.AlphaNumeric(9);

    public DateTime GetValidDateOfBirth()
        => Faker.Person.DateOfBirth;

    public string GetValidPassword()
        => "ValidPassword123!";

    public bool GetRandomBoolean()
    => new Random().NextDouble() < 0.5;

    public User GetExampleUser()
        => new(
            GetValidUserName(),
            GetValidEmail(),
            GetValidPhone(),
            GetValidCPF(),
            GetValidDateOfBirth(),
            GetValidRG(),
            GetValidPassword()
        );

    public List<User> GetExampleUserList(int lenght = 10)
        => Enumerable.Range(1, lenght)
            .Select(_ => GetExampleUser()).ToList();

    public User GetValidUserWithoutPassword()
        => new(
            GetValidUserName(),
            GetValidEmail(),
            GetValidPhone(),
            GetValidCPF(),
            GetValidDateOfBirth(),
            GetValidRG(),
            string.Empty
        );


    public FiapTechChalengeDbContext CreateDbContext()
        => new FiapTechChalengeDbContext(
            new DbContextOptionsBuilder<FiapTechChalengeDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
        );


}
