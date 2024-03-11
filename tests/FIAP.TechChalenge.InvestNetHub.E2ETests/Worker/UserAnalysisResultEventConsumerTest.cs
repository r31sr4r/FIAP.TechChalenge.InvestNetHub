using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.Common;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.DTOs;
using FluentAssertions;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Worker;

[Collection(nameof(UserBaseFixture))]
public class UserAnalysisResultEventConsumerTest
    : IDisposable
{
    private readonly UserBaseFixture _fixture;

    public UserAnalysisResultEventConsumerTest(UserBaseFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(AnalysisResultSuccededEventReceived))]
    [Trait("E2E/Worker", "UserAnalysisResult - Event Handler")]
    public async Task AnalysisResultSuccededEventReceived()
    {
        var investmentPreferences = "{\"assetTypes\": [\"Stocks\", \"Bonds\"], \"investmentHorizon\": \"LongTerm\", \"interestedSectors\": [\"Technology\", \"Healthcare\"]}";
        var exampleUsersList = _fixture.GeUsersList(5);
        await _fixture.Persistence.InsertList(exampleUsersList);
        var user = exampleUsersList[2];
        var exampleEvent = new UserAnalysisResultMessageDTO
        {
            User = new UserAnalysisResultMetadataDTO
            {
                ResourceId = user.Id.ToString(),
                RiskLevel = user.RiskLevel.ToString(),
                InvestmentPreferences = user.InvestmentPreferences
            }
        };

        _fixture.PublishMessageToRabbitMQ(exampleEvent);

        await Task.Delay(1000);

        var dbUser = await _fixture.Persistence
            .GetById(user.Id);
        dbUser.Should().NotBeNull();
        dbUser!.AnalysisStatus.Should().Be(AnalysisStatus.Completed);
        dbUser.RiskLevel.Should().Be(RiskLevel.Low);
        dbUser.InvestmentPreferences.Should().Be(investmentPreferences);
        (object? @event, uint count) = _fixture.ReadMessageFromRabbitMQ<object>();
        @event.Should().NotBeNull();
        count.Should().Be(0);
    }

    [Fact(DisplayName = nameof(AnalysisResultFailEventReceived))]
    [Trait("E2E/Worker", "UserAnalysisResult - Event Handler")]
    public async Task AnalysisResultFailEventReceived()
    {
        var investmentPreferences = "{\"assetTypes\": [\"Stocks\", \"Bonds\"], \"investmentHorizon\": \"LongTerm\", \"interestedSectors\": [\"Technology\", \"Healthcare\"]}";
        var exampleUsersList = _fixture.GeUsersList(5);
        await _fixture.Persistence.InsertList(exampleUsersList);
        var user = exampleUsersList[2];
        var exampleEvent = new UserAnalysisResultMessageDTO
        {
            Message = new UserAnalysisResultMetadataDTO
            {
                ResourceId = user.Id.ToString(),
                RiskLevel = user.RiskLevel.ToString(),
                InvestmentPreferences = user.InvestmentPreferences
            },
            Error = "There was an error on the analysis."
        };

        _fixture.PublishMessageToRabbitMQ(exampleEvent);

        await Task.Delay(1000);

        var dbUser = await _fixture.Persistence
            .GetById(user.Id);
        dbUser.Should().NotBeNull();
        dbUser!.AnalysisStatus.Should().Be(AnalysisStatus.Error);
        dbUser.RiskLevel.Should().Be(RiskLevel.Undefined);
        dbUser.InvestmentPreferences.Should().BeNull();
        (object? @event, uint count) = _fixture.ReadMessageFromRabbitMQ<object>();
        @event.Should().NotBeNull();
        count.Should().Be(0);
    }

    [Fact(DisplayName = nameof(InvalidMessageEventReceived))]
    [Trait("E2E/Worker", "UserAnalysisResult - Event Handler")]
    public async Task InvalidMessageEventReceived()
    {
        var investmentPreferences = "{\"assetTypes\": [\"Stocks\", \"Bonds\"], \"investmentHorizon\": \"LongTerm\", \"interestedSectors\": [\"Technology\", \"Healthcare\"]}";
        var exampleUsersList = _fixture.GeUsersList(5);
        await _fixture.Persistence.InsertList(exampleUsersList);
        var exampleEvent = new UserAnalysisResultMessageDTO
        {
            Message = new UserAnalysisResultMetadataDTO
            {
                ResourceId = Guid.NewGuid().ToString(),
                RiskLevel = RiskLevel.Undefined.ToString(),
                InvestmentPreferences = "Invalid"
            },
            Error = "There was an error on the analysis."
        };

        _fixture.PublishMessageToRabbitMQ(exampleEvent);

        await Task.Delay(1000);

        (object? @event, uint count) = _fixture.ReadMessageFromRabbitMQ<object>();
        @event.Should().BeNull();
        count.Should().Be(0);
    }


    public void Dispose()
    {
        _fixture.CleanPersistence();
        _fixture.PurgeRabbitMQQueues();
    }
}
