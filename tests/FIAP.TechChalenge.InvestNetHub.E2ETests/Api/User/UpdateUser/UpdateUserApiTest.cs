﻿using FIAP.TechChalenge.InvestNetHub.Api.ApiModels.Response;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.UpdateUser;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.UpdateUser;

[Collection(nameof(UpdateUserApiTestFixture))]
public class UpdateUserApiTest
{
    private readonly UpdateUserApiTestFixture _fixture;

    public UpdateUserApiTest(UpdateUserApiTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(UpdateUser))]
    [Trait("E2E/Api", "User/Update - Endpoints")]
    public async Task UpdateUser()
    {
        var exampleUsersList = _fixture.GeUsersList(20);
        await _fixture.Persistence.InsertList(exampleUsersList);
        var exampleUser = exampleUsersList[10];
        var userModelInput = _fixture.GetInput(exampleUser.Id);

        var (response, output) = await _fixture
            .ApiClient
            .Put<ApiResponse<UserModelOutput>>(
            $"/users/{exampleUser.Id}",
            userModelInput
        );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
        output.Should().NotBeNull();
        output!.Data.Name.Should().Be(userModelInput.Name);
        output.Data.Email.Should().Be(userModelInput.Email);
        output.Data.Phone.Should().Be(userModelInput.Phone);
        output.Data.CPF.Should().Be(userModelInput.CPF);
        output.Data.DateOfBirth.Should().Be(userModelInput.DateOfBirth);
        output.Data.RG.Should().Be(userModelInput.RG);
        output.Data.IsActive.Should().Be(userModelInput.IsActive);
        output.Data.Id.Should().NotBeEmpty();
        output.Data.Id.Should().Be(exampleUser.Id);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        var dbUser = await _fixture.Persistence
            .GetById(exampleUser.Id);
        dbUser.Should().NotBeNull();
        dbUser!.Name.Should().Be(userModelInput.Name);
        dbUser.Email.Should().Be(userModelInput.Email);
        dbUser.Phone.Should().Be(userModelInput.Phone);
        dbUser.CPF.Should().Be(userModelInput.CPF);
        dbUser.DateOfBirth.Date.Should().Be(userModelInput.DateOfBirth.Date);
        dbUser.RG.Should().Be(userModelInput.RG);
        dbUser.IsActive.Should().Be(userModelInput.IsActive);
        dbUser.Id.Should().NotBeEmpty();
    }
}
