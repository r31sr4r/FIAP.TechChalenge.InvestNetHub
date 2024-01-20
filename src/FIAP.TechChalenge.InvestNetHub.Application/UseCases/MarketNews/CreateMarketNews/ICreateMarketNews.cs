namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;
public interface ICreateMarketNews
{
    public Task<CreateMarketNewsOutput> Handle(
        CreateMarketNewsInput input, 
        CancellationToken cancellationToken
    );
}
