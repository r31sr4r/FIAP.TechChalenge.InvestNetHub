using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;
public class CreatePortfolioInput : IRequest<PortfolioModelOutput>
{
    public CreatePortfolioInput(
        string userId,
        string name,
        string description
    )
    {
        UserId = userId;
        Name = name;
        Description = description;
    }

    public string UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
