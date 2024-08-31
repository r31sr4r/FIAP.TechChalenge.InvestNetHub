using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeleteAssetFromPortfolio
{
    public class DeleteAssetFromPortfolioInput : IRequest
    {
        public DeleteAssetFromPortfolioInput(Guid portfolioId, Guid assetId)
        {
            PortfolioId = portfolioId;
            AssetId = assetId;
        }

        public Guid PortfolioId { get; set; }
        public Guid AssetId { get; set; }
    }
}
