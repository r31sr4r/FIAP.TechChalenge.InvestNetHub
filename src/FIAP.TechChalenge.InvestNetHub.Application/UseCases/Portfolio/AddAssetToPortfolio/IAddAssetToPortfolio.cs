using MediatR;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddAssetToPortfolio;

public interface IAddAssetToPortfolio : IRequestHandler<AddAssetToPortfolioInput, AssetModelOutput>
{
}
