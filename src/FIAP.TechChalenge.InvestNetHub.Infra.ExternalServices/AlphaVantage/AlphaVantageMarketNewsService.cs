using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Common;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.AlphaVantage
{
    public class AlphaVantageMarketNewsService
        : ApiClientBase, IMarketNewsService
    {
        private const string providerName = "AlphaVantage";
        private readonly IMarketNewsMapper _mapper;

        public AlphaVantageMarketNewsService(IMarketNewsMapper mapper)
            : base(providerName)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<MarketNewsDto>> GetMarketNewsAsync(
            string tickers,
            string topics,
            DateTime? fromTime,
            DateTime? toTime,
            string sort = "LATEST",
            int limit = 50)
        {
            var url = BuildApiUrl(tickers, topics, fromTime, toTime, sort, limit);

            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<dynamic>(content);

            var newsList = _mapper.MapMarketNews(json);

            return newsList;
        }

        private string BuildApiUrl(
            string tickers,
            string topics,
            DateTime? fromTime,
            DateTime? toTime,
            string sort,
            int limit)
        {
            var url = $"{BaseUrl}?function={NewsFunction}&tickers={tickers}&apikey={ApiKey}";
            url += !string.IsNullOrEmpty(topics) ? $"&topics={topics}" : "";
            url += fromTime.HasValue ? $"&time_from={fromTime.Value:yyyyMMddTHHmm}" : "";
            url += toTime.HasValue ? $"&time_to={toTime.Value:yyyyMMddTHHmm}" : "";
            url += $"&sort={sort}&limit={limit}";

            return url;
        }
    }
}
