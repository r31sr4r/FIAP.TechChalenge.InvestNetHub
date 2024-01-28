﻿using UseCase = FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.AuthUser;
using FluentAssertions;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.User.AuthUser;

[Collection(nameof(AuthUserTestFixture))]
public class AuthUserTest
{
    private readonly AuthUserTestFixture _fixture;

    public AuthUserTest(AuthUserTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "GetByEmail_ReturnsUser_WhenUserExists")]
    [Trait("Integration/Application", "AuthUser - Use Cases")]
    public async Task GetByEmail_ReturnsUser_WhenUserExists()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleUser = _fixture.GetValidUser();
        dbContext.Add(exampleUser);
        dbContext.SaveChanges();
        var userRepository = new UserRepository(dbContext);

        var input = new UseCase.AuthUserInput(
            exampleUser.Email, _fixture.GetValidPassword());

        var useCase = new UseCase.AuthUser(userRepository);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Email.Should().Be(exampleUser.Email);
    }

    [Fact(DisplayName = "GetByEmail_ThrowsNotFoundException_WhenUserDoesNotExist")]
    [Trait("Integration/Application", "AuthUser - Use Cases")]
    public async Task GetByEmail_ThrowsNotFoundException_WhenUserDoesNotExist()
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var userRepository = new UserRepository(dbContext);
        var nonExistingEmail = "nonexisting@example.com";
        var input = new UseCase.AuthUserInput(
            nonExistingEmail, _fixture.GetValidPassword());

        var useCase = new UseCase.AuthUser(userRepository);

        var task = async () 
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"User with email {nonExistingEmail} not found");
    }
}