using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;

namespace FIAP.TechChalenge.InvestNetHub.Domain.Repository;
public interface IUserRepository
    : IGenericRepository<User>,
    ISearchableRepository<User>
{
    public Task<IReadOnlyList<Guid>> GetIdsListByIds(
    List<Guid> ids,
    CancellationToken cancellationToken
);
}
