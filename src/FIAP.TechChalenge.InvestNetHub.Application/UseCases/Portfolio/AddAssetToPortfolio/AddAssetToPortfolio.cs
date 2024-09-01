using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddAssetToPortfolio;

public class AddAssetToPortfolio : IAddAssetToPortfolio
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddAssetToPortfolio(
        IPortfolioRepository portfolioRepository,
        IUnitOfWork unitOfWork
    )
    {
        _portfolioRepository = portfolioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AssetModelOutput> Handle(AddAssetToPortfolioInput request, CancellationToken cancellationToken)
    {
        var portfolio = await _portfolioRepository.Get(request.PortfolioId, cancellationToken);
        if (portfolio == null)
        {
            throw new EntityNotFoundException($"Portfolio with id {request.PortfolioId} not found");
        }

        if (!Enum.TryParse<AssetType>(request.Type, true, out var assetType))
        {
            throw new EntityValidationException($"Invalid asset type: {request.Type}");
        }        

        var asset = new Asset(
            request.PortfolioId,
            assetType,
            request.Name,
            request.Code,
            request.Quantity,
            request.Price
        );

        portfolio.AddAsset(asset);

        await _portfolioRepository.AddAssetAsync(request.PortfolioId, asset, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return AssetModelOutput.FromAsset(asset);
    }
}
