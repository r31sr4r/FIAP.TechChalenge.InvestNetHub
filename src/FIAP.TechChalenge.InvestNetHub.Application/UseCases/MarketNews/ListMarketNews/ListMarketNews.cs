using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Models;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
public class ListMarketNews : IListMarketNews
{
    private readonly IMarketNewsService _marketNewsService;

    public ListMarketNews(IMarketNewsService marketNewsService)
    {
        _marketNewsService = marketNewsService;
    }

    public async Task<ListMarketNewsOutput> Handle(
        ListMarketNewsInput request,
        CancellationToken cancellationToken)
    {
        var marketNews = await _marketNewsService.GetMarketNewsAsync(
            request.Tickers,
            request.Topics,
            request.FromTime,
            request.ToTime,
            request.Sort,
            request.Limit,
            cancellationToken
        );

        var marketNewsOutputList = marketNews
            .Select(MarketNewsModelOutput.FromDto)
            .ToList();

        return new ListMarketNewsOutput(marketNewsOutputList);
    }
}
