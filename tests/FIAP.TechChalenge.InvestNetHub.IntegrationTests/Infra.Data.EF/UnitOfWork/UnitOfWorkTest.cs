using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UnitOfWorkInfra = FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.Data.EF.UnitOfWork;

[Collection(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTest
{
    private readonly UnitOfWorkTestFixture _fixture;

    public UnitOfWorkTest(UnitOfWorkTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Commit")]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task Commit()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleUserList = _fixture.GetExampleUserList();
        await dbContext.Users.AddRangeAsync(exampleUserList);
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);

        await unitOfWork.Commit(CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(true);
        var users = await assertDbContext.Users.AsNoTracking().ToListAsync();

        users.Should().HaveCount(exampleUserList.Count);
    }

    [Fact(DisplayName = "Rollback")]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task Rollback()
    {
        var dbContext = _fixture.CreateDbContext();
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);

        var task = async () => await unitOfWork.Rollback(CancellationToken.None);

        await task.Should().NotThrowAsync();
    }
}
