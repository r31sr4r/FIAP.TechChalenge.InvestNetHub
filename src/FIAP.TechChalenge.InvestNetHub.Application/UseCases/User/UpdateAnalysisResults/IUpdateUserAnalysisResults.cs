using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.UpdateAnalysisResults;
public interface IUpdateUserAnalysisResults
    : IRequestHandler<UpdateUserAnalysisResultsInput, UserModelOutput>
{
}
