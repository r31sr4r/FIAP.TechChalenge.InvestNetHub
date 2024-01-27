namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.User.CreateUser;

[Collection(nameof(CreateUserTestFixture))]
public class CreateUserTest
{
    private readonly CreateUserTestFixture _fixture;

    public CreateUserTest(CreateUserTestFixture fixture)
    {
        _fixture = fixture;
    }



}
