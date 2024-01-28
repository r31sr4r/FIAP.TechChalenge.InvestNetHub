using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
public class MarketNewsRepository
    : IMarketNewsRepository
{
    public Task Delete(MarketNews aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<MarketNews> Get(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Insert(MarketNews aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SearchOutput<MarketNews>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(MarketNews aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
