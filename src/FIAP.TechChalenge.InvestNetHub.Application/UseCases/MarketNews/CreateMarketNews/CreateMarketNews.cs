using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;
public class CreateMarketNews : ICreateMarketNews
{
    private readonly IMarketNewsRepository _marketNewsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMarketNews(
        IMarketNewsRepository marketNewsRepository, 
        IUnitOfWork unitOfWork)
    {
        _marketNewsRepository = marketNewsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateMarketNewsOutput> Handle(
        CreateMarketNewsInput input, 
        CancellationToken cancellationToken)
    {
        var marketNews = new DomainEntity.MarketNews(
            input.Title,
            input.Summary,
            input.PublishDate,
            input.Url,
            input.Source,
            input.ImageUrl,
            input.Authors,
            input.OverallSentimentScore,
            input.OverallSentimentLabel
        );

        await _marketNewsRepository.Insert(marketNews, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return CreateMarketNewsOutput.FromMarketNews(marketNews);            

    }
}
