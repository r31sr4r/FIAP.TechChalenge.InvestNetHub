namespace FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
public interface IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken);
}
