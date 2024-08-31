using MediatR;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddTransactionToPortfolio;

public interface IAddTransactionToPortfolio : IRequestHandler<AddTransactionToPortfolioInput, TransactionModelOutput>
{
}
