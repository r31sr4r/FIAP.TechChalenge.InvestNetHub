namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
public class PortfolioModelOutput
{
    public PortfolioModelOutput(
        Guid id,
        Guid userId,
        string name,
        string description,
        DateTime createdAt
    )
    {
        Id = id;
        UserId = userId;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static PortfolioModelOutput FromPortfolio(Domain.Entity.Portfolio portfolio)
    {
        return new PortfolioModelOutput(
            portfolio.Id,
            portfolio.UserId,
            portfolio.Name,
            portfolio.Description,
            portfolio.CreatedAt
        );
    }
}
