using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.RemoveTransactionFromPortfolio
{
    public class RemoveTransactionFromPortfolio : IRemoveTransactionFromPortfolio
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTransactionFromPortfolio(
            IPortfolioRepository portfolioRepository,
            IUnitOfWork unitOfWork)
        {
            _portfolioRepository = portfolioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveTransactionFromPortfolioInput request, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolioRepository.Get(request.PortfolioId, cancellationToken);
            if (portfolio == null)
            {
                throw new EntityNotFoundException($"Portfolio with id {request.PortfolioId} not found");
            }

            var transaction = portfolio.Transactions.FirstOrDefault(t => t.Id == request.TransactionId);
            if (transaction == null)
            {
                throw new EntityNotFoundException($"Transaction with id {request.TransactionId} not found in portfolio {request.PortfolioId}");
            }

            portfolio.RemoveTransaction(transaction);

            await _portfolioRepository.RemoveTransactionAsync(request.PortfolioId, request.TransactionId, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return Unit.Value;
        }
    }
}
