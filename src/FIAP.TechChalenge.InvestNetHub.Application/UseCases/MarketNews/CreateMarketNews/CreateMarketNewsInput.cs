namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;
public class CreateMarketNewsInput
{
    public CreateMarketNewsInput(
        string title,
        string summary,
        DateTime publishDate,
        string url,
        string source,
        string imageUrl,
        List<string> authors,
        decimal overallSentimentScore,
        string overallSentimentLabel
        )
    {
        Title = title;
        Summary = summary;
        PublishDate = publishDate;
        Url = url;
        Source = source;
        ImageUrl = imageUrl;
        Authors = authors;
        OverallSentimentScore = overallSentimentScore;
        OverallSentimentLabel = overallSentimentLabel;
    }

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
