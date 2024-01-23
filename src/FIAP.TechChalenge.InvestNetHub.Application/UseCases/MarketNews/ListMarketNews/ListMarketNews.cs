using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
public class ListMarketNews
    : IListMarketNews
{
    private readonly IMarketNewsRepository _marketNewsRepository;

    public ListMarketNews(IMarketNewsRepository marketNewsRepository) 
        => _marketNewsRepository = marketNewsRepository;

    public async Task<ListMarketNewsOutput> Handle(
        ListMarketNewsInput request, 
        CancellationToken cancellationToken)
    {
        var seachOutput = await _marketNewsRepository.Search(
            new (
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir
            ),
            cancellationToken
        );

        return new ListMarketNewsOutput(
            seachOutput.CurrentPage,
            seachOutput.PerPage,
            seachOutput.Total,
            seachOutput.Items
                .Select(MarketNewsModelOutput.FromMarketNews)
                .ToList()
        );
    }
}
