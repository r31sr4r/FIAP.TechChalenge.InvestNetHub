using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeletePortfolio;
public class DeletePortfolio : IDeletePortfolio
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePortfolio(IPortfolioRepository portfolioRepository, IUnitOfWork unitOfWork)
        => (_portfolioRepository, _unitOfWork) = (portfolioRepository, unitOfWork);

    public async Task<Unit> Handle(DeletePortfolioInput request, CancellationToken cancellationToken)
    {
        var portfolio = await _portfolioRepository.Get(request.Id, cancellationToken);
        await _portfolioRepository.Delete(portfolio, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}
