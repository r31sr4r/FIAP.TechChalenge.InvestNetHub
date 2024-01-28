using MediatR;
using System;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
public class ListMarketNewsInput : IRequest<ListMarketNewsOutput>
{
    public string Tickers { get; }
    public string Topics { get; }
    public DateTime? FromTime { get; }
    public DateTime? ToTime { get; }
    public string Sort { get; }
    public int Limit { get; }

    public ListMarketNewsInput(
        string tickers,
        string topics,
        DateTime? fromTime = null,
        DateTime? toTime = null,
        string sort = "LATEST",
        int limit = 50)
    {
        Tickers = tickers;
        Topics = topics;
        FromTime = fromTime;
        ToTime = toTime;
        Sort = sort;
        Limit = limit;
    }
}
