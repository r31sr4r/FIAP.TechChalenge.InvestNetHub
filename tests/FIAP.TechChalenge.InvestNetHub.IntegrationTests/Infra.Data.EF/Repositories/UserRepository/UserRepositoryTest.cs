using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FluentAssertions;
using Repository = FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.Data.EF.Repositories.UserRepository;

[Collection(nameof(UserRepositoryTestFixture))]
public class UserRepositoryTest
{
    private readonly UserRepositoryTestFixture _fixture;

    public UserRepositoryTest(UserRepositoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Insert")]
    [Trait("Integration/Infra.Data", "UserRepository - Repositories")]
    public async Task Insert()
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var exampleUser = _fixture.GetExampleUser();
        var userRepository = new Repository.UserRepository(dbContext);

        await userRepository.Insert(exampleUser, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var dbUser = await dbContext.Users.FindAsync(exampleUser.Id);

        dbUser.Should().NotBeNull();
        dbUser.Id.Should().Be(exampleUser.Id);
        dbUser.Name.Should().Be(exampleUser.Name);
        dbUser.Email.Should().Be(exampleUser.Email);
        dbUser.Phone.Should().Be(exampleUser.Phone);
        dbUser.CPF.Should().Be(exampleUser.CPF);
        dbUser.DateOfBirth.Date.Should().Be(exampleUser.DateOfBirth.Date);
        dbUser.RG.Should().Be(exampleUser.RG);
        dbUser.IsActive.Should().Be(exampleUser.IsActive);
    }

    [Fact(DisplayName = "Get")]
    [Trait("Integration/Infra.Data", "UserRepository - Repositories")]
    public async Task Get()
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var exampleUser = _fixture.GetExampleUser();
        var exampleUserList = _fixture.GetExampleUserList(15);
        exampleUserList.Add(exampleUser);
        await dbContext.AddRangeAsync(exampleUserList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var userRepository = new Repository.UserRepository(dbContext);
                
        var dbUser = await userRepository.Get(exampleUser.Id, CancellationToken.None);

        dbUser.Should().NotBeNull();
        dbUser.Id.Should().Be(exampleUser.Id);
        dbUser.Name.Should().Be(exampleUser.Name);
        dbUser.Email.Should().Be(exampleUser.Email);
        dbUser.Phone.Should().Be(exampleUser.Phone);
        dbUser.CPF.Should().Be(exampleUser.CPF);
        dbUser.DateOfBirth.Date.Should().Be(exampleUser.DateOfBirth.Date);
        dbUser.RG.Should().Be(exampleUser.RG);
        dbUser.IsActive.Should().Be(exampleUser.IsActive);
    }


}
