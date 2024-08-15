using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Configurations
{
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(transaction => transaction.Id);

            builder.Property(transaction => transaction.PortfolioId)
                .IsRequired();

            builder.Property(transaction => transaction.AssetId)
                .IsRequired();

            builder.Property(transaction => transaction.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(transaction => transaction.Quantity)
                .IsRequired();

            builder.Property(transaction => transaction.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(transaction => transaction.TransactionDate)
                .IsRequired();
        }
    }
}
