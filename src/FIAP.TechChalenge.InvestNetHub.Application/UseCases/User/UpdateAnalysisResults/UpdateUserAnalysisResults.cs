using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.UpdateAnalysisResults;
public class UpdateUserAnalysisResults
    : IUpdateUserAnalysisResults
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateUserAnalysisResults> _logger;

    public UpdateUserAnalysisResults(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork, 
        ILogger<UpdateUserAnalysisResults> logger)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UserModelOutput> Handle(
        UpdateUserAnalysisResultsInput request, 
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.UserId, cancellationToken);

        switch (request.Status)
        {
        case AnalysisStatus.Completed:
            user.UpdateAnalysisResults(
                request.RiskLevel,
                request.InvestmentPreferences!,
                request.AnalysisDate!.Value);
            break;
        case AnalysisStatus.Error:
            _logger.LogError(
                "Error on analysis for user {UserId}. Error: {ErrorMessage}",
                user.Id, request.ErrorMessage);
            user.UpdateAnalysisAsError();
            break;
        default:
            throw new EntityValidationException("Invalid Analysis Status");
        }

        await _userRepository.Update(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return UserModelOutput.FromUser(user);
    }
}
