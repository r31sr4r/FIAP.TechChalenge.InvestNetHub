using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;
public class CreatePortfolio : ICreatePortfolio
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePortfolio(
        IPortfolioRepository portfolioRepository,
        IUnitOfWork unitOfWork
    )
    {
        _portfolioRepository = portfolioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PortfolioModelOutput> Handle(CreatePortfolioInput request, CancellationToken cancellationToken)
    {
        var portfolio = new DomainEntity.Portfolio(
            request.UserId,
            request.Name,
            request.Description
        );

        await _portfolioRepository.Insert(portfolio, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return PortfolioModelOutput.FromPortfolio(portfolio);
    }
}
