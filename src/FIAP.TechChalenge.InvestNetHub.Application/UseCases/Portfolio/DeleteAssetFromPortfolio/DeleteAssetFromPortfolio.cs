using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeleteAssetFromPortfolio
{
    public class DeleteAssetFromPortfolio : IDeleteAssetFromPortfolio
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAssetFromPortfolio(
            IPortfolioRepository portfolioRepository,
            IUnitOfWork unitOfWork)
        {
            _portfolioRepository = portfolioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteAssetFromPortfolioInput request, CancellationToken cancellationToken)
        {
            await _portfolioRepository.RemoveAssetAsync(request.PortfolioId, request.AssetId, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
            return Unit.Value;
        }
    }
}
