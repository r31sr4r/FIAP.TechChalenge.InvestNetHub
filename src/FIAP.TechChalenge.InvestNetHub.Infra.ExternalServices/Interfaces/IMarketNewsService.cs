using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Models;

namespace FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Interfaces
{
    public interface IMarketNewsService
    {
        Task<IEnumerable<MarketNewsDto>> GetMarketNewsAsync(
            string tickers, 
            string topics, 
            DateTime? fromTime, 
            DateTime? toTime, 
            string sort = "LATEST",
            int limit = 50);
    }
}
