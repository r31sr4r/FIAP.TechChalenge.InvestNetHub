using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
using FIAP.TechChalenge.InvestNetHub.Domain.Validation;
using System;

namespace FIAP.TechChalenge.InvestNetHub.Domain.Entity;

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
        DomainValidation.NotNullOrEmpty(Title, nameof(Title));
        DomainValidation.NotNullOrEmpty(Summary, nameof(Summary));
        DomainValidation.DateNotInFuture(PublishDate, nameof(PublishDate));
        DomainValidation.NotNullOrEmpty(Url, nameof(Url));
        DomainValidation.NotNullOrEmpty(Source, nameof(Source));
        
        if (!ValidateAuthors(Authors))
            throw new EntityValidationException($"{nameof(Authors)} list cannot be empty.");
    }

    private bool ValidateAuthors(List<string> authors)
    {
        return authors != null && authors.Any();
    }

}

