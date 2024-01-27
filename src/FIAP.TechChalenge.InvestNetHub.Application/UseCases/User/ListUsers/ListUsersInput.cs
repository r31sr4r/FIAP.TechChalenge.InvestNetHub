using FIAP.TechChalenge.InvestNetHub.Application.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.ListUsers;
public class ListUsersInput
    : PaginatedListInput,
    IRequest<ListUsersOutput>
{
    public ListUsersInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc)
        : base(page, perPage, search, sort, dir)
    { }

    public ListUsersInput()
        : base(1, 15, "", "", SearchOrder.Asc)
    { }
}
