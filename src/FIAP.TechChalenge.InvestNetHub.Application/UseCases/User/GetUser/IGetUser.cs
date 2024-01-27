using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.GetUser;
public interface IGetUser :
    IRequestHandler<GetUserInput, UserModelOutput>
{
}
