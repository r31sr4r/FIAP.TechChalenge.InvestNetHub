using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.GetPortfolio;
public class GetPortfolioInput : IRequest<PortfolioModelOutput>
{
    public GetPortfolioInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
