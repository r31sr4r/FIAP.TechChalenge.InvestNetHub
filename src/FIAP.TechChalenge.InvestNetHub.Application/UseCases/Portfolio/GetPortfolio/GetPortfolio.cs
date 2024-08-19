using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.GetPortfolio;
public class GetPortfolio : IGetPortfolio
{
    private readonly IPortfolioRepository _repository;

    public GetPortfolio(IPortfolioRepository repository)
    {
        _repository = repository;
    }

    public async Task<PortfolioModelOutput> Handle(
        GetPortfolioInput request,
        CancellationToken cancellationToken
        )
    {
        var portfolio = await _repository.Get(request.Id, cancellationToken);
        return PortfolioModelOutput.FromPortfolio(portfolio);
    }
}
