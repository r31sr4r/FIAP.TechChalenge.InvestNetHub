using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.RemoveTransactionFromPortfolio
{
    public class RemoveTransactionFromPortfolioInput : IRequest
    {
        public RemoveTransactionFromPortfolioInput(Guid portfolioId, Guid transactionId)
        {
            PortfolioId = portfolioId;
            TransactionId = transactionId;
        }

        public Guid PortfolioId { get; set; }
        public Guid TransactionId { get; set; }
    }
}
