using MediatR;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddTransactionToPortfolio;

public class AddTransactionToPortfolioInput : IRequest<TransactionModelOutput>
{
    public AddTransactionToPortfolioInput(
        Guid portfolioId,
        Guid assetId,
        string type,
        int quantity,
        decimal price,
        DateTime transactionDate
    )
    {
        PortfolioId = portfolioId;
        AssetId = assetId;
        Type = type;
        Quantity = quantity;
        Price = price;
        TransactionDate = transactionDate;
    }

    public Guid PortfolioId { get; set; }
    public Guid AssetId { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime TransactionDate { get; set; }
}
