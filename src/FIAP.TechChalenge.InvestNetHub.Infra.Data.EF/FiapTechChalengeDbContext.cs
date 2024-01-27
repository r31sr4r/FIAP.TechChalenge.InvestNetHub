using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF;
public class FiapTechChalengeDbContext 
    : DbContext
{
    public DbSet<User> Users => Set<User>();

    public FiapTechChalengeDbContext(
        DbContextOptions<FiapTechChalengeDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}

