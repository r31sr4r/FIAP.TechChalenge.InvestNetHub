using MediatR;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddAssetToPortfolio;

public class AddAssetToPortfolioInput : IRequest<AssetModelOutput>
{
    public AddAssetToPortfolioInput(
        Guid portfolioId,
        string name,
        string code,
        string type,
        int quantity,
        decimal price
    )
    {
        PortfolioId = portfolioId;
        Name = name;
        Code = code;
        Type = type;
        Quantity = quantity;
        Price = price;
    }

    public Guid PortfolioId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
