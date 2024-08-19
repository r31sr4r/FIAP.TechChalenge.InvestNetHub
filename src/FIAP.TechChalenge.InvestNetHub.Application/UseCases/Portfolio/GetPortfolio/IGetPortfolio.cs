using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.GetPortfolio;
public interface IGetPortfolio :
    IRequestHandler<GetPortfolioInput, PortfolioModelOutput>
{
}
