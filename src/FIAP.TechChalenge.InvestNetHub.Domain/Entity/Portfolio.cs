using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
using System.Collections.Generic;

namespace FIAP.TechChalenge.InvestNetHub.Domain.Entity;

public class Portfolio : AggregateRoot
{
    private readonly List<Asset> _assets = new();
    private readonly List<Transaction> _transactions = new();

    public Portfolio(string userId, string name, string description)
    {
        UserId = userId;
        Name = name;
        Description = description;
        CreatedAt = DateTime.Now;

        Validate();
    }

    public string UserId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IReadOnlyCollection<Asset> Assets => _assets.AsReadOnly();
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

    public void AddAsset(Asset asset)
    {
        if (_assets.Any(a => a.Code == asset.Code))
            throw new BusinessRuleException($"The asset with code {asset.Code} already exists in the portfolio.");       
        _assets.Add(asset);
    }

    public void RemoveAsset(Asset asset)
    {
        if (_transactions.Any(t => t.AssetId == asset.Id))
            throw new BusinessRuleException($"The asset with code {asset.Code} cannot be removed as it has associated transactions.");
        _assets.Remove(asset);
    }

    public void AddTransaction(Transaction transaction)
    {
        if (!_assets.Any(a => a.Id == transaction.AssetId))
            throw new BusinessRuleException($"Cannot add transaction. Asset with ID {transaction.AssetId} not found in portfolio.");
        _transactions.Add(transaction);
    }

    public void RemoveTransaction(Transaction transaction)
    {
        if (transaction.TransactionDate < DateTime.Now.AddMonths(-1))
            throw new BusinessRuleException($"Transaction from {transaction.TransactionDate} cannot be removed.");

        _transactions.Remove(transaction);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(UserId))
            throw new EntityValidationException($"{nameof(UserId)} should not be empty or null");
        if (string.IsNullOrWhiteSpace(Name))
            throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
        if (Name.Length <= 3)
            throw new EntityValidationException($"{nameof(Name)} should be greater than 3 characters");
        if (Name.Length >= 255)
            throw new EntityValidationException($"{nameof(Name)} should be less than 255 characters");
        if (string.IsNullOrWhiteSpace(Description))
            throw new EntityValidationException($"{nameof(Description)} should not be empty or null");
    }
}
