using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using EntitySeedWork = FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Domain.Entity;

public class Transaction : EntitySeedWork
{
    public Transaction(Guid portfolioId, Guid assetId, TransactionType type, int quantity, decimal price, DateTime transactionDate)
    {
        PortfolioId = portfolioId;
        AssetId = assetId;
        Type = type;
        Quantity = quantity;
        Price = price;
        TransactionDate = transactionDate;

        Validate();
    }

    public Guid PortfolioId { get; private set; }
    public Guid AssetId { get; private set; }
    public TransactionType Type { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public DateTime TransactionDate { get; private set; }

    private void Validate()
    {
        if (Quantity <= 0)
            throw new EntityValidationException($"{nameof(Quantity)} should be greater than 0");
        if (Price <= 0)
            throw new EntityValidationException($"{nameof(Price)} should be greater than 0");
    }
}
