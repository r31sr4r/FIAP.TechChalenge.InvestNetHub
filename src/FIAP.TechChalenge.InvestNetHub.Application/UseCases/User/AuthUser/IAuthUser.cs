using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.GetUser;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.AuthUser;
public interface IAuthUser :
    IRequestHandler<AuthUserInput, UserModelOutput>
{
}
