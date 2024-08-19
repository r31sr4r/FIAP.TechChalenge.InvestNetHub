using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;

public interface IUpdatePortfolio
    : IRequestHandler<UpdatePortfolioInput, PortfolioModelOutput>
{ }
