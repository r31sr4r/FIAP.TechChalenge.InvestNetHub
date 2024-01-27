using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Update;
public interface IUpdateUser
    : IRequestHandler<UpdateUserInput, UserModelOutput>
{ }

