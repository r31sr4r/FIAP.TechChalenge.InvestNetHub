using FIAP.TechChalenge.InvestNetHub.Application;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UseCase = FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.UpdateAnalysisResults;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.User.UpdateUserAnalysisResults;

[Collection(nameof(UpdateUserAnalysisResultsTestFixture))]
public class UpdateUserAnalysisResultsTest
{
    private readonly UpdateUserAnalysisResultsTestFixture _fixture;

    public UpdateUserAnalysisResultsTest(UpdateUserAnalysisResultsTestFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(UpdateUserAnalysisResults))]
    [Trait("Integration/Application", "UpdateUserAnalysisResults - Use Cases")]
    public async Task UpdateUserAnalysisResults()
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new UserRepository(dbContext);
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var eventPublisher = new DomainEventPublisher(serviceProvider);
        var unitOfWork = new UnitOfWork(
            dbContext,
            eventPublisher,
            serviceProvider.GetRequiredService<ILogger<UnitOfWork>>()
        );
        var userExample = _fixture.GetValidUser();
        await dbContext.AddRangeAsync(_fixture.GeUsersList());
        var tracking = await dbContext.AddAsync(userExample);
        dbContext.SaveChanges();
        tracking.State = EntityState.Detached;
        var useCase = new UseCase.UpdateUserAnalysisResults(
            repository, 
            unitOfWork,
            serviceProvider.GetRequiredService<ILogger<UseCase.UpdateUserAnalysisResults>>()
            );
        var expectedAnalysisStatus = AnalysisStatus.Completed;
        var expectedRiskLevel = RiskLevel.Medium;
        var expectedInvestmentPreferences = "{\"assetTypes\": [\"Stocks\", \"Bonds\"], \"investmentHorizon\": \"LongTerm\", \"interestedSectors\": [\"Technology\", \"Healthcare\"]}";

        var input = new UseCase.UpdateUserAnalysisResultsInput(
            userExample.Id,
            expectedAnalysisStatus,
            expectedRiskLevel,
            expectedInvestmentPreferences,
            DateTime.Now
        );

        await useCase.Handle(input, CancellationToken.None);

        var dbUser = await (_fixture.CreateDbContext(true))
            .Users
            .FindAsync(userExample.Id);

        dbUser.Should().NotBeNull();
        dbUser!.AnalysisStatus.Should().Be(expectedAnalysisStatus);
        dbUser.RiskLevel.Should().Be(expectedRiskLevel);
        dbUser.InvestmentPreferences.Should().Be(expectedInvestmentPreferences);
    }

    [Fact(DisplayName = nameof(ThrowWhenUserNotFound))]
    [Trait("Integration/Application", "UpdateUserAnalysisResults - Use Cases")]
    public async Task ThrowWhenUserNotFound()
    {
        var dbContext = _fixture.CreateDbContext();
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var eventPublisher = new DomainEventPublisher(serviceProvider);
        var unitOfWork = new UnitOfWork(
            dbContext,
            eventPublisher,
            serviceProvider.GetRequiredService<ILogger<UnitOfWork>>()
        );
        var exampleUser = _fixture.GetValidUser();
        dbContext.Add(exampleUser);
        dbContext.SaveChanges();
        var userRepository = new UserRepository(dbContext);
        var expectedAnalysisStatus = AnalysisStatus.Completed;
        var expectedRiskLevel = RiskLevel.Medium;
        var expectedInvestmentPreferences = "{\"assetTypes\": [\"Stocks\", \"Bonds\"], \"investmentHorizon\": \"LongTerm\", \"interestedSectors\": [\"Technology\", \"Healthcare\"]}";

        var input = new UseCase.UpdateUserAnalysisResultsInput(
            Guid.NewGuid(),
            expectedAnalysisStatus,
            expectedRiskLevel,
            expectedInvestmentPreferences,
            DateTime.Now
        );
        var useCase = new UseCase.UpdateUserAnalysisResults(
            userRepository,
            unitOfWork,
            serviceProvider.GetRequiredService<ILogger<UseCase.UpdateUserAnalysisResults>>()
            );


        var task = async ()
            => await useCase.Handle(input, CancellationToken.None
        );

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"User with id {input.UserId} not found");

    }
}
