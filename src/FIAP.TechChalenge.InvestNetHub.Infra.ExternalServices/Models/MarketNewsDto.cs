namespace FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Models;

public class MarketNewsDto
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public DateTime PublishDate { get; set; }
    public string Url { get; set; }
    public string Source { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Authors { get; set; }
    public decimal OverallSentimentScore { get; set; }
    public string OverallSentimentLabel { get; set; }
}

