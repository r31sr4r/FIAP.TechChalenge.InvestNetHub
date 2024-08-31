using FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;

public class AssetModelOutput
{
    public AssetModelOutput(Guid id, string name, string code, string type, int quantity, decimal price)
    {
        Id = id;
        Name = name;
        Code = code;
        Type = type;
        Quantity = quantity;
        Price = price;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public static AssetModelOutput FromAsset(Asset asset)
    {
        return new AssetModelOutput(
            asset.Id,
            asset.Name,
            asset.Code,
            asset.Type.ToString(),
            asset.Quantity,
            asset.Price
        );
    }
}
