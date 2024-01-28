using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;


namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.Common;
public class UserPersistence
{
    private readonly FiapTechChalengeDbContext _context;

    public UserPersistence(FiapTechChalengeDbContext context)
        => _context = context;

    public async Task<DomainEntity.User?> GetById(Guid id)
        => await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task InsertList(List<DomainEntity.User> categories)
    {
        await _context.Users.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
    }
}
