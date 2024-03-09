using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.UpdateAnalysisResults;
public record UpdateUserAnalysisResultsInput(
    Guid UserId,
    AnalysisStatus Status,
    RiskLevel RiskLevel,
    string? InvestmentPreferences,
    DateTime? AnalysisDate,
    string? ErrorMessage = null
    ) : IRequest<UserModelOutput>;

