namespace FIAP.TechChalenge.InvestNetHub.Api.ApiModels.Portfolio;

public class AddTransactionToPortfolioApiInput
{
    public Guid AssetId { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
