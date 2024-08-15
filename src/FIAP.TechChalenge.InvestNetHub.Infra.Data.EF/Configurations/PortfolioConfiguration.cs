using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Configurations
{
    internal class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasKey(portfolio => portfolio.Id);

            builder.Property(portfolio => portfolio.UserId)
                .IsRequired();

            builder.Property(portfolio => portfolio.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(portfolio => portfolio.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(portfolio => portfolio.CreatedAt)
                .IsRequired();

            builder.HasMany(p => p.Assets)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Transactions)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(portfolio => portfolio.Events);
        }
    }
}
