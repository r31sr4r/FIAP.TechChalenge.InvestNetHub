using FIAP.TechChalenge.InvestNetHub.Application.Common;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.ListUsers;
public class ListUsersOutput
    : PaginatedListOutput<UserModelOutput>
{
    public ListUsersOutput(
        int page,
        int perPage,
        int total,
        IReadOnlyList<UserModelOutput> items)
        : base(page, perPage, total, items)
    { }
}

