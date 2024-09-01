namespace FIAP.TechChalenge.InvestNetHub.Api.ApiModels.Portfolio;

public class AddAssetToPortfolioApiInput
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}