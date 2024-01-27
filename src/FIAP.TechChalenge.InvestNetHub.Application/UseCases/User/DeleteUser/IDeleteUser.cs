using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.DeleteUser;
public interface IDeleteUser
    : IRequestHandler<DeleteUserInput>
{ }
