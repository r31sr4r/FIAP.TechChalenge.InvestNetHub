using FIAP.TechChalenge.InvestNetHub.Application.Common;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.Common;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
public class ListMarketNewsOutput
    : PaginatedListOutput<MarketNewsModelOutput>
{
    public ListMarketNewsOutput(
        int page, 
        int pageSize, 
        int total, 
        IReadOnlyList<MarketNewsModelOutput> items) 
        : base(page, pageSize, total, items)
    {
    }
}
