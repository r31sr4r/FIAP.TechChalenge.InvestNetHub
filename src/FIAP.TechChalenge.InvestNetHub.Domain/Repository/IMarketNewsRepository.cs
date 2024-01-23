using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;

namespace FIAP.TechChalenge.InvestNetHub.Domain.Repository;
public interface IMarketNewsRepository 
    : IGenericRepository<MarketNews>,
    ISearchableRepository<MarketNews>
{ }
