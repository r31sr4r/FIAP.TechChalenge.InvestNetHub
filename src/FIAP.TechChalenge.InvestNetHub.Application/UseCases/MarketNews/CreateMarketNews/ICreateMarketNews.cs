using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;
public interface ICreateMarketNews 
    : IRequestHandler<CreateMarketNewsInput, CreateMarketNewsOutput>
{
    public Task<CreateMarketNewsOutput> Handle(
        CreateMarketNewsInput input, 
        CancellationToken cancellationToken
    );
}
