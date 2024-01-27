using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;
public class UserRepository
    : IUserRepository
{
    private readonly FiapTechChalengeDbContext _context;
    private DbSet<User> _users => _context.Set<User>();

    public UserRepository(FiapTechChalengeDbContext context)
    {
        _context = context;
    }

    public async Task Insert(User aggregate, CancellationToken cancellationToken)
        => await _users.AddAsync(aggregate, cancellationToken);

    public async Task<User> Get(Guid id, CancellationToken cancellationToken)
        => await _users.FindAsync(new object[] { id }, cancellationToken);

    public Task Delete(User aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    public Task<User> GetByEmail(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Guid>> GetIdsListByIds(List<Guid> ids, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    public Task<SearchOutput<User>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(User aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
