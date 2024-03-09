using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.User.Common;
using System;
using Newtonsoft.Json;
using Xunit.Sdk;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.UpdateAnalysisResults;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.User.UpdateUserAnalysisResults;

[CollectionDefinition(nameof(UpdateUserAnalysisResultsTestFixture))]
public class UpdateUserAnalysisResultsTestFixtureCollecton
    : ICollectionFixture<UpdateUserAnalysisResultsTestFixture>
{ }

public class UpdateUserAnalysisResultsTestFixture : UserUseCasesBaseFixture
{
    public UpdateUserAnalysisResultsInput GetSucceededAnalysisInput(Guid userId)
    {
        var preferences = new
        {
            assetTypes = new[] { "Stocks", "Bonds" },
            investmentHorizon = "LongTerm",
            interestedSectors = new[] { "Technology", "Healthcare" }
        };

        string investmentPreferencesJson = JsonConvert.SerializeObject(preferences);

        return new UpdateUserAnalysisResultsInput(
            userId,
            AnalysisStatus.Completed,
            RiskLevel.High,
            investmentPreferencesJson,
            DateTime.UtcNow
        );
    }

    public UpdateUserAnalysisResultsInput GetFailedAnalysisInput(Guid userId)
    {
        return new UpdateUserAnalysisResultsInput(
            userId,
            AnalysisStatus.Error,
            RiskLevel.Undefined, 
            "", 
            DateTime.UtcNow,
            ErrorMessage: "There was an error while analyzing the user."
        );
    }

    public UpdateUserAnalysisResultsInput GetInvalidStatusInput(Guid userId)
    {
        return new UpdateUserAnalysisResultsInput(
            userId,
            AnalysisStatus.Pending,
            RiskLevel.Undefined,
            "",
            DateTime.UtcNow,
            ErrorMessage: "There was an error while analyzing the user."
        );
    }
}
