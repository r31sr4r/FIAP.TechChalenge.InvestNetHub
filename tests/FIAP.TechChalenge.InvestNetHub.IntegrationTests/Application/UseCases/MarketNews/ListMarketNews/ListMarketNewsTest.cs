using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.AlphaVantage;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Common;
using FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using UseCase = FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.MarketNews.ListMarketNews
{
    [Collection(nameof(ListMarketNewsTestFixture))]
    public class ListMarketNewsTest
    {
        private readonly ListMarketNewsTestFixture _fixture;
        private readonly IMarketNewsService _marketNewsService;

        public ListMarketNewsTest(ListMarketNewsTestFixture fixture)
        {
            _fixture = fixture;
            var logger = new LoggerFactory().CreateLogger<MarketNewsMapper>();
            var mapper = new MarketNewsMapper(logger);
            _marketNewsService = new AlphaVantageMarketNewsService(mapper);
        }

        [Fact(DisplayName = "ShouldReturnNewsList")]
        [Trait("Integration/Application", "ListMarketNews - Use Cases")]
        public async Task ShouldReturnNewsList()
        {
            var randomInput = _fixture.GetRandomInput();

            var useCase = new UseCase.ListMarketNews(_marketNewsService);

            var output = await useCase.Handle(randomInput, CancellationToken.None);

            output.Should().NotBeNull();        
            
            var input = _fixture.GetInput();

            output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();

        }


    }
}
