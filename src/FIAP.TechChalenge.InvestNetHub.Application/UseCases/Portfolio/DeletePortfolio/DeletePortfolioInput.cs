using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeletePortfolio;
public class DeletePortfolioInput : IRequest
{
    public DeletePortfolioInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
