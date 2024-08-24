using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;
public class CreatePortfolioInput : IRequest<PortfolioModelOutput>
{
    public CreatePortfolioInput(
        Guid userId,
        string name,
        string description
    )
    {
        UserId = userId;
        Name = name;
        Description = description;
    }

    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
