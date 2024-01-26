using System.Dynamic;
using System.Collections.Generic;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Models;

namespace FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Interfaces
{
    public interface IMarketNewsMapper
    {
        IEnumerable<MarketNewsDto> MapMarketNews(dynamic json);
    }
}
