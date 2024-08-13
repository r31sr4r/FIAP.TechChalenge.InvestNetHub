using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using EntitySeedWork = FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Domain.Entity;

public class Asset : EntitySeedWork
{
    public Asset(AssetType type, string name, string code)
    {
        Type = type;
        Name = name;
        Code = code;

        Validate();
    }

    public AssetType Type { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
        if (Name.Length <= 3)
            throw new EntityValidationException($"{nameof(Name)} should be greater than 3 characters");
        if (Name.Length >= 255)
            throw new EntityValidationException($"{nameof(Name)} should be less than 255 characters");
        if (string.IsNullOrWhiteSpace(Code))
            throw new EntityValidationException($"{nameof(Code)} should not be empty or null");
    }
}
