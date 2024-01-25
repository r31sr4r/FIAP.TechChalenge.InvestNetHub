using FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Interfaces
{
    public interface IMarketNewsService
    {
        Task<IEnumerable<MarketNews>> GetMarketNewsAsync(string tickers, string topics, DateTime? fromTime, DateTime? toTime);
    }
}
