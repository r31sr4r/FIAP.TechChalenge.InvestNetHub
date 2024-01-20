using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;

namespace FIAP.TechChalenge.InvestNetHub.Domain.Entity
{
    public class MarketNews : AggregateRoot
    {
        public MarketNews(
            string title,
            string summary,
            DateTime publishDate,
            string url,
            string source,
            string imageUrl,
            List<string> authors,
            decimal overallSentimentScore,
            string overallSentimentLabel
        ) : base()
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

            Validate();
        }

        public string Title { get; private set; }
        public string Summary { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Url { get; private set; }
        public string Source { get; private set; }
        public string ImageUrl { get; private set; }
        public List<string> Authors { get; private set; }
        public decimal OverallSentimentScore { get; private set; }
        public string OverallSentimentLabel { get; private set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
                throw new EntityValidationException("Title cannot be empty or null.");

            if (string.IsNullOrWhiteSpace(Summary))
                throw new EntityValidationException("Summary cannot be empty or null.");

            if (!ValidatePublishDate(PublishDate))            
                throw new EntityValidationException("Publish date cannot be in the future.");

            if (string.IsNullOrWhiteSpace(Url))
                throw new EntityValidationException("URL cannot be empty or null.");

            if (string.IsNullOrWhiteSpace(Source))
                throw new EntityValidationException("Source cannot be empty or null.");

            if (!ValidateAuthors(Authors))
                throw new EntityValidationException("Authors list cannot be empty.");
        }

        private bool ValidatePublishDate(DateTime publishDate)
        {
            return publishDate <= DateTime.Now;
        }

        private bool ValidateAuthors(List<string> authors)
        {
            return authors != null && authors.Any();
        }

    }
}
