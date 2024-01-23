using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.Common;
public class MarketNewsModelOutput
{
    public MarketNewsModelOutput(
        Guid id, 
        string title,
        string summary,
        DateTime publishDate,
        string url,
        string source,
        string imageUrl,
        List<string> authors,
        decimal overallSentimentScore,
        string overallSentimentLabel)
    {
        Id = id;
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

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public DateTime PublishDate { get; set; }
    public string Url { get; set; }
    public string Source { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Authors { get; set; }
    public decimal OverallSentimentScore { get; set; }
    public string OverallSentimentLabel { get; set; }

    public static MarketNewsModelOutput FromMarketNews(DomainEntity.MarketNews marketNews)
    {
        return new MarketNewsModelOutput(
            marketNews.Id,
            marketNews.Title,
            marketNews.Summary,
            marketNews.PublishDate,
            marketNews.Url,
            marketNews.Source,
            marketNews.ImageUrl,
            marketNews.Authors,
            marketNews.OverallSentimentScore,
            marketNews.OverallSentimentLabel
            );
    }

}
