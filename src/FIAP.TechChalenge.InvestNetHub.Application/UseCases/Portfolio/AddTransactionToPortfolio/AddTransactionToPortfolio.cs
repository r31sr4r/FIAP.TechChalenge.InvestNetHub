
using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddTransactionToPortfolio;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddTransactionToPortfolio;

public class AddTransactionToPortfolio : IAddTransactionToPortfolio
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTransactionToPortfolio(
        IPortfolioRepository portfolioRepository,
        IUnitOfWork unitOfWork
    )
    {
        _portfolioRepository = portfolioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionModelOutput> Handle(AddTransactionToPortfolioInput request, CancellationToken cancellationToken)
    {
        var portfolio = await _portfolioRepository.Get(request.PortfolioId, cancellationToken);
        if (portfolio == null)
        {
            throw new EntityNotFoundException($"Portfolio with id {request.PortfolioId} not found");
        }

        var asset = portfolio.Assets.FirstOrDefault(a => a.Id == request.AssetId);
        if (asset == null)
        {
            throw new EntityNotFoundException($"Asset with id {request.AssetId} not found in the portfolio");
        }

        if (!Enum.TryParse<TransactionType>(request.Type, true, out var transactionType))
        {
            throw new EntityValidationException($"Invalid asset type: {request.Type}");
        }    

        var transaction = new Transaction(
            request.PortfolioId,
            request.AssetId,
            transactionType,
            request.Quantity,
            request.Price,
            request.TransactionDate
        );

        // Aplica a transação ao ativo
        asset.ApplyTransaction(transaction);

        // Salva a transação no repositório
        await _portfolioRepository.AddTransactionAsync(request.PortfolioId, transaction, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return TransactionModelOutput.FromTransaction(transaction);
    }
}
