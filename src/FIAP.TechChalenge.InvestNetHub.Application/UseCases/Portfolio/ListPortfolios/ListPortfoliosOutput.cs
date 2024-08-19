using FIAP.TechChalenge.InvestNetHub.Application.Common;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.ListPortfolios;
public class ListPortfoliosOutput
    : PaginatedListOutput<PortfolioModelOutput>
{
    public ListPortfoliosOutput(
        int page,
        int perPage,
        int total,
        IReadOnlyList<PortfolioModelOutput> items)
        : base(page, perPage, total, items)
    { }
}
