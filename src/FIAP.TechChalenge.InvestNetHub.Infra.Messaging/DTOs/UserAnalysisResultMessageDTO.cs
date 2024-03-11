using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Messaging.DTOs;
public class UserAnalysisResultMessageDTO
{
    public UserAnalysisResultMetadataDTO? User { get; set; }
    public UserAnalysisResultMetadataDTO? Message { get; set; }
    public string? Error { get; set; }
}

public class UserAnalysisResultMetadataDTO
{
    public string? ResourceId { get; set; }
    public string? RiskLevel { get; set; }
    public string? InvestmentPreferences { get; set; }
}