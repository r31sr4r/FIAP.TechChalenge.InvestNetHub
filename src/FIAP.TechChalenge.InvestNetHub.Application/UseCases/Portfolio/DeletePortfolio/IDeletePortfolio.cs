using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeletePortfolio;
public interface IDeletePortfolio
    : IRequestHandler<DeletePortfolioInput>
{ }
