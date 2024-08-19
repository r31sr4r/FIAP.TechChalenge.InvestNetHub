using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;

public class UpdatePortfolio
    : IUpdatePortfolio
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePortfolio(
        IPortfolioRepository portfolioRepository,
        IUnitOfWork unitOfWork)
        => (_portfolioRepository, _unitOfWork)
            = (portfolioRepository, unitOfWork);


    public async Task<PortfolioModelOutput> Handle(UpdatePortfolioInput request, CancellationToken cancellationToken)
    {
        var portfolio = await _portfolioRepository.Get(request.Id, cancellationToken);
        portfolio.Update(
            request.Name,
            request.Description
        );

        await _portfolioRepository.Update(portfolio, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return PortfolioModelOutput.FromPortfolio(portfolio);
    }
}
