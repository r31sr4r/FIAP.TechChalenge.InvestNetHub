using FIAP.TechChalenge.InvestNetHub.Application.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
public class ListMarketNewsInput
    : PaginatedListInput, IRequest<ListMarketNewsOutput>
{
    public ListMarketNewsInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc)
        : base(page, perPage, search, sort, dir)
    {
    }
}
