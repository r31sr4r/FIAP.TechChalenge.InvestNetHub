using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;

public class UpdatePortfolioInput : IRequest<PortfolioModelOutput>
{
    public UpdatePortfolioInput(
        Guid id,
        string name,
        string description
    )
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
