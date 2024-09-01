using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly FiapTechChalengeDbContext _context;
        private DbSet<Portfolio> _portfolios => _context.Set<Portfolio>();
        private DbSet<Asset> _assets => _context.Set<Asset>();
        private DbSet<Transaction> _transactions => _context.Set<Transaction>();

        public PortfolioRepository(FiapTechChalengeDbContext context)
        {
            _context = context;
        }

        public async Task Insert(Portfolio aggregate, CancellationToken cancellationToken)
            => await _portfolios.AddAsync(aggregate, cancellationToken);

        public async Task<Portfolio> Get(Guid id, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolios
                .Include(p => p.Assets)
                .Include(p => p.Transactions)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            NotFoundException.ThrowIfNull(portfolio, $"Portfolio with id {id} not found");
            return portfolio!;
        }

        public Task Update(Portfolio aggregate, CancellationToken _)
            => Task.FromResult(_portfolios.Update(aggregate));

        public Task Delete(Portfolio aggregate, CancellationToken _)
            => Task.FromResult(_portfolios.Remove(aggregate));

        public async Task<SearchOutput<Portfolio>> Search(
            SearchInput input,
            CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PerPage;
            var query = _portfolios.AsNoTracking();
            query = AddSorting(query, input.OrderBy, input.Order);
            if (!string.IsNullOrWhiteSpace(input.Search))
                query = query.Where(x => x.Name.Contains(input.Search));

            var total = await query.CountAsync();
            var items = await query
                .Skip(toSkip)
                .Take(input.PerPage)
                .ToListAsync();

            return new SearchOutput<Portfolio>(
                currentPage: input.Page,
                perPage: input.PerPage,
                total: total,
                items: items
            );
        }

        private IQueryable<Portfolio> AddSorting(
            IQueryable<Portfolio> query,
            string orderProperty,
            SearchOrder order
        )
        {
            var orderedEnumerable = (orderProperty, order) switch
            {
                ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name),
                ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name),
                ("createdAt", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
                ("createdAt", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
                _ => query.OrderBy(x => x.Name)
            };

            return orderedEnumerable
                .ThenBy(x => x.CreatedAt);
        }

        public async Task AddAssetAsync(Guid portfolioId, Asset asset, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolios
                .Include(p => p.Assets)
                .FirstOrDefaultAsync(p => p.Id == portfolioId, cancellationToken);

            NotFoundException.ThrowIfNull(portfolio, $"Portfolio with id {portfolioId} not found");

            await _assets.AddAsync(asset, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }



        public async Task RemoveAssetAsync(Guid portfolioId, Guid assetId, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolios.Include(p => p.Assets).FirstOrDefaultAsync(p => p.Id == portfolioId, cancellationToken);

            NotFoundException.ThrowIfNull(portfolio, $"Portfolio with id {portfolioId} not found");

            var asset = portfolio.Assets.FirstOrDefault(a => a.Id == assetId);
            if (asset == null)
                throw new BusinessRuleException($"Asset with id {assetId} not found in the portfolio");

            portfolio.RemoveAsset(asset);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddTransactionAsync(Guid portfolioId, Transaction transaction, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolios.FindAsync(new object[] { portfolioId }, cancellationToken);

            NotFoundException.ThrowIfNull(portfolio, $"Portfolio with id {portfolioId} not found");

            await _transactions.AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveTransactionAsync(Guid portfolioId, Guid transactionId, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolios.Include(p => p.Transactions).FirstOrDefaultAsync(p => p.Id == portfolioId, cancellationToken);

            NotFoundException.ThrowIfNull(portfolio, $"Portfolio with id {portfolioId} not found");

            var transaction = portfolio.Transactions.FirstOrDefault(t => t.Id == transactionId);
            if (transaction == null)
                throw new BusinessRuleException($"Transaction with id {transactionId} not found in the portfolio");

            portfolio.RemoveTransaction(transaction);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
