using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.CreateUser;
public interface ICreateUser :
    IRequestHandler<CreateUserInput, UserModelOutput>
{
}
