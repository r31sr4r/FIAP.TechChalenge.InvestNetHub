using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;
public interface ICreatePortfolio :
    IRequestHandler<CreatePortfolioInput, PortfolioModelOutput>
{
}
