using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;
public interface ICreateMarketNews 
    : IRequestHandler<CreateMarketNewsInput, MarketNewsModelOutput>
{ }
