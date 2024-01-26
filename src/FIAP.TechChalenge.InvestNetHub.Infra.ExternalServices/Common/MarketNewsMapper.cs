using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Models;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Common
{
    public class MarketNewsMapper : IMarketNewsMapper
    {
        private readonly ILogger<MarketNewsMapper> _logger;

        public MarketNewsMapper(ILogger<MarketNewsMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<MarketNewsDto> MapMarketNews(dynamic json)
        {
            try
            {
                if (json == null || json["feed"] == null)
                {
                    _logger.LogWarning("JSON feed is null or missing.");
                    return Enumerable.Empty<MarketNewsDto>();
                }

                var newsList = new List<MarketNewsDto>();
                foreach (var item in json["feed"])
                {
                    try
                    {
                        var dateTimeString = (string)item["time_published"];
                        var publishDate = DateTime.ParseExact(dateTimeString, "yyyyMMddTHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                        var news = new MarketNewsDto
                        {
                            Title = item.title,
                            Summary = item.summary,
                            PublishDate = publishDate,
                            Url = item.url,
                            Source = item.source,
                            ImageUrl = item.banner_image,
                            Authors = item.authors.ToObject<List<string>>(),
                            OverallSentimentScore = item.overall_sentiment_score,
                            OverallSentimentLabel = item.overall_sentiment_label
                        };
                        newsList.Add(news);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error mapping news item: {ex.Message}");
                    }
                }

                return newsList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error mapping market news: {ex.Message}");
                return Enumerable.Empty<MarketNewsDto>();
            }
        }
    }
}
