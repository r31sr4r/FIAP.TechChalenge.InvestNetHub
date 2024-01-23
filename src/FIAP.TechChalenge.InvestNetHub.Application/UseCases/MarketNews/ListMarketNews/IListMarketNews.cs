using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
public interface IListMarketNews
    : IRequestHandler<ListMarketNewsInput, ListMarketNewsOutput>
{}

