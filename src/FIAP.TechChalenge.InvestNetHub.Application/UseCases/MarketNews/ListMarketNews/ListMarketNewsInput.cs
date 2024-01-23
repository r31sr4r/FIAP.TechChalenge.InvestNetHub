using FIAP.TechChalenge.InvestNetHub.Application.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
public class ListMarketNewsInput
    : PaginatedListInput, IRequest<ListMarketNewsOutput>
{
    public ListMarketNewsInput(
        int page,
        int pageSize,
        string search
        , string sort,
        SearchOrder dir) 
        : base(page, pageSize, search, sort, dir)
    {
    }
}
