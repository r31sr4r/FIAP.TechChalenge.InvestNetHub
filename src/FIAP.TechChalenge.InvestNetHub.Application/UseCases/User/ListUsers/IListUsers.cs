using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.ListUsers;
public interface IListUsers
    : IRequestHandler<ListUsersInput, ListUsersOutput>
{
}
