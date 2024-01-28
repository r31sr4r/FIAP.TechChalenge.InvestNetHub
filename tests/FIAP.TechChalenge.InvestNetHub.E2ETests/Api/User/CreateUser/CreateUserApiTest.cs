using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.CreateUser;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.CreateUser;

[Collection(nameof(CreateUserApiTestFixture))]
public class CreateUserApiTest
{
    private readonly CreateUserApiTestFixture _fixture;

    public CreateUserApiTest(CreateUserApiTestFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateUser))]
    [Trait("E2E/Api", "User/Create - Endpoints")]
    public async Task CreateUser()
    {
        var input = _fixture.GetInput();

        var (response, output) = await _fixture
            .ApiClient
            .Post<UserModelOutput>(
                "/users", 
                input
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        output.Should().NotBeNull();
        output!.Name.Should().Be(input.Name);
        output.Email.Should().Be(input.Email);
        output.Phone.Should().Be(input.Phone);
        output.CPF.Should().Be(input.CPF);
        output.DateOfBirth.Should().Be(input.DateOfBirth);
        output.RG.Should().Be(input.RG);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default);

        var dbUser = await _fixture.Persistence
            .GetById(output.Id);
        dbUser.Should().NotBeNull();
        dbUser!.Name.Should().Be(input.Name);
        dbUser.Email.Should().Be(input.Email);
        dbUser.Phone.Should().Be(input.Phone);
        dbUser.CPF.Should().Be(input.CPF);
        dbUser.DateOfBirth.Date.Should().Be(input.DateOfBirth.Date);
        dbUser.RG.Should().Be(input.RG);
        dbUser.IsActive.Should().Be(input.IsActive);
        dbUser.Id.Should().NotBeEmpty();
    }

    [Theory(DisplayName = nameof(ErrorWhenCantInstatiateAggregate))]
    [Trait("E2E/Api", "User/Create - Endpoints")]
    [MemberData(
        nameof(UpdateUserApiTestDataGenerator.GetInvalidInputs),
        MemberType = typeof(UpdateUserApiTestDataGenerator)
    )]
    public async Task ErrorWhenCantInstatiateAggregate(
        CreateUserInput input,
        string expectedDetail
    )
    {
        var (response, output) = await _fixture
            .ApiClient
            .Post<ProblemDetails>(
                "/users",
                input
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        output.Should().NotBeNull();
        output!.Title.Should().Be("One or more validation errors occurred");
        output.Type.Should().Be("UnprocessableEntity");
        output.Status.Should().Be((int)StatusCodes.Status422UnprocessableEntity);
        output.Detail.Should().Be(expectedDetail);
    }

}
