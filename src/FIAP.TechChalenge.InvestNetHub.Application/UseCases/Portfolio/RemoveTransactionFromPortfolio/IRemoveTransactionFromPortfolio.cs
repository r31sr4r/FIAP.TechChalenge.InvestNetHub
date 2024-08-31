using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.RemoveTransactionFromPortfolio
{
    public interface IRemoveTransactionFromPortfolio : IRequestHandler<RemoveTransactionFromPortfolioInput>
    {
    }
}
