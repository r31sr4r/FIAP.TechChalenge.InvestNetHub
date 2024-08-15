using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Configurations
{
    internal class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(asset => asset.Id);

            builder.Property(asset => asset.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(asset => asset.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(asset => asset.Type)
                .HasConversion<int>()
                .IsRequired();
        }
    }
}
