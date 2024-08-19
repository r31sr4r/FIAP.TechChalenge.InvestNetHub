using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.ListPortfolios;
public class ListPortfolios
    : IListPortfolios
{
    private readonly IPortfolioRepository _portfolioRepository;

    public ListPortfolios(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<ListPortfoliosOutput> Handle(
        ListPortfoliosInput request,
        CancellationToken cancellationToken)
    {
        var searchOutput = await _portfolioRepository.Search(
            new(
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir
                ),
            cancellationToken
        );
        return new ListPortfoliosOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                .Select(PortfolioModelOutput.FromPortfolio)
                .ToList()
        );
    }
}
