using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;

namespace FIAP.TechChalenge.InvestNetHub.Domain.Repository
{
    public interface IPortfolioRepository 
        : IGenericRepository<Portfolio>, ISearchableRepository<Portfolio>
    { 
        Task AddAssetAsync(Guid portfolioId, Asset asset, CancellationToken cancellationToken);
        Task RemoveAssetAsync(Guid portfolioId, Guid assetId, CancellationToken cancellationToken);

        Task AddTransactionAsync(Guid portfolioId, Transaction transaction, CancellationToken cancellationToken);
        Task RemoveTransactionAsync(Guid portfolioId, Guid transactionId, CancellationToken cancellationToken);
    }
}
