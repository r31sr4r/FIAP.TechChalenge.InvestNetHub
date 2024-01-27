using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
public class UnitOfWork
    : IUnitOfWork
{
    private readonly FiapTechChalengeDbContext _dbContext;

    public UnitOfWork(FiapTechChalengeDbContext dbContext) 
        => _dbContext = dbContext;

    public Task Commit(CancellationToken cancellationToken)
        => _dbContext.SaveChangesAsync(cancellationToken);

    public Task Rollback(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
