using FIAP.TechChalenge.InvestNetHub.Application.Common;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.Common;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
public class ListMarketNewsOutput
{
    public IEnumerable<MarketNewsModelOutput> MarketNews { get; }

    public ListMarketNewsOutput(IEnumerable<MarketNewsModelOutput> marketNews)
    {
        MarketNews = marketNews;
    }
}
