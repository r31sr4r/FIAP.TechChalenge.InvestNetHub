using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using EntitySeedWork = FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Domain.Entity;

public class Asset : EntitySeedWork
{
    public Asset(Guid portfolioId, AssetType type, string name, string code, int quantity, decimal price)
    {
        PortfolioId = portfolioId;
        Type = type;
        Name = name;
        Code = code;
        Quantity = quantity;
        Price = price;

        Validate();
    }

    public Guid PortfolioId { get; private set; } 
    public AssetType Type { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public void UpdateQuantity(int quantity)
    {
        if (quantity < 0)
            throw new EntityValidationException($"{nameof(Quantity)} cannot be negative");

        Quantity = quantity;
    }

    private void Validate()
    {
        if (PortfolioId == Guid.Empty)
            throw new EntityValidationException($"{nameof(PortfolioId)} should not be empty");
        if (string.IsNullOrWhiteSpace(Name))
            throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
        if (Name.Length <= 3)
            throw new EntityValidationException($"{nameof(Name)} should be greater than 3 characters");
        if (Name.Length >= 255)
            throw new EntityValidationException($"{nameof(Name)} should be less than 255 characters");
        if (string.IsNullOrWhiteSpace(Code))
            throw new EntityValidationException($"{nameof(Code)} should not be empty or null");
        if (Quantity < 0)
            throw new EntityValidationException($"{nameof(Quantity)} should not be negative");
        if (Price <= 0)
            throw new EntityValidationException($"{nameof(Price)} should be greater than 0");
    }

    public void ApplyTransaction(Transaction transaction)
    {
        if (transaction.Type == TransactionType.Buy)
        {
            UpdateQuantity(Quantity + transaction.Quantity);
        }
        else if (transaction.Type == TransactionType.Sell)
        {
            UpdateQuantity(Quantity - transaction.Quantity);
        }
    }
}
