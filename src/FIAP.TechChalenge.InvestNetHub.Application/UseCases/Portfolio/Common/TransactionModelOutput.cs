using FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;

public class TransactionModelOutput
{
    public TransactionModelOutput(
        Guid id,
        Guid portfolioId,
        Guid assetId,
        string type,
        int quantity,
        decimal price,
        DateTime transactionDate
    )
    {
        Id = id;
        PortfolioId = portfolioId;
        AssetId = assetId;
        Type = type;
        Quantity = quantity;
        Price = price;
        TransactionDate = transactionDate;
    }

    public Guid Id { get; set; }
    public Guid PortfolioId { get; set; }
    public Guid AssetId { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime TransactionDate { get; set; }

    public static TransactionModelOutput FromTransaction(Transaction transaction)
    {
        return new TransactionModelOutput(
            transaction.Id,
            transaction.PortfolioId,
            transaction.AssetId,
            transaction.Type.ToString(),
            transaction.Quantity,
            transaction.Price,
            transaction.TransactionDate
        );
    }
}
