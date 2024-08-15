using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Repository = FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Repositories;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Infra.Data.EF.Repositories.PortfolioRepository;

[Collection(nameof(PortfolioRepositoryTestFixture))]
public class PortfolioRepositoryTest
{
    private readonly PortfolioRepositoryTestFixture _fixture;

    public PortfolioRepositoryTest(PortfolioRepositoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Insert")]
    [Trait("Integration/Infra.Data", "PortfolioRepository - Repositories")]
    public async Task Insert()
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var examplePortfolio = _fixture.GetValidPortfolio();
        var portfolioRepository = new Repository.PortfolioRepository(dbContext);

        await portfolioRepository.Insert(examplePortfolio, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var dbPortfolio = await (_fixture.CreateDbContext(true))            
            .Portfolios
            .Include(p => p.Assets)
            .Include(p => p.Transactions)
            .FirstOrDefaultAsync(p => p.Id == examplePortfolio.Id);

        dbPortfolio.Should().NotBeNull();
        dbPortfolio!.Id.Should().Be(examplePortfolio.Id);
        dbPortfolio.Name.Should().Be(examplePortfolio.Name);
        dbPortfolio.Description.Should().Be(examplePortfolio.Description);
    }

    [Fact(DisplayName = "Get")]
    [Trait("Integration/Infra.Data", "PortfolioRepository - Repositories")]
    public async Task Get()
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var examplePortfolio = _fixture.GetValidPortfolio();
        await dbContext.Portfolios.AddAsync(examplePortfolio);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var portfolioRepository = new Repository.PortfolioRepository(_fixture.CreateDbContext(true));

        var dbPortfolio = await portfolioRepository.Get(examplePortfolio.Id, CancellationToken.None);

        dbPortfolio.Should().NotBeNull();
        dbPortfolio.Id.Should().Be(examplePortfolio.Id);
        dbPortfolio.Name.Should().Be(examplePortfolio.Name);
        dbPortfolio.Description.Should().Be(examplePortfolio.Description);
    }

    [Fact(DisplayName = "GetThrowIfNotFound")]
    [Trait("Integration/Infra.Data", "PortfolioRepository - Repositories")]
    public async Task GetThrowIfNotFound()
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var exampleId = Guid.NewGuid();
        var portfolioRepository = new Repository.PortfolioRepository(dbContext);

        var task = async () => await portfolioRepository.Get(exampleId, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Portfolio with id {exampleId} not found");
    }

    [Fact(DisplayName = "Update")]
    [Trait("Integration/Infra.Data", "PortfolioRepository - Repositories")]
    public async Task Update()
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var examplePortfolio = _fixture.GetValidPortfolio();
        var newPortfolio = _fixture.GetValidPortfolio();
        await dbContext.Portfolios.AddAsync(examplePortfolio);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var portfolioRepository = new Repository.PortfolioRepository(dbContext);

        examplePortfolio.Update(newPortfolio.Name, newPortfolio.Description);
        await portfolioRepository.Update(examplePortfolio, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var dbPortfolio = await (_fixture.CreateDbContext(true))
            .Portfolios
            .Include(p => p.Assets)
            .Include(p => p.Transactions)
            .FirstOrDefaultAsync(p => p.Id == examplePortfolio.Id);

        dbPortfolio.Should().NotBeNull();
        dbPortfolio!.Id.Should().Be(examplePortfolio.Id);
        dbPortfolio.Name.Should().Be(examplePortfolio.Name);
        dbPortfolio.Description.Should().Be(examplePortfolio.Description);
    }

    [Fact(DisplayName = "Delete")]
    [Trait("Integration/Infra.Data", "PortfolioRepository - Repositories")]
    public async Task Delete()
    {
        FiapTechChalengeDbContext dbContext = _fixture.CreateDbContext();
        var examplePortfolio = _fixture.GetValidPortfolio();
        await dbContext.Portfolios.AddAsync(examplePortfolio);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var portfolioRepository = new Repository.PortfolioRepository(dbContext);

        await portfolioRepository.Delete(examplePortfolio, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var dbPortfolio = await (_fixture.CreateDbContext(true))
            .Portfolios
            .FirstOrDefaultAsync(p => p.Id == examplePortfolio.Id);

        dbPortfolio.Should().BeNull();
    }

   
}
