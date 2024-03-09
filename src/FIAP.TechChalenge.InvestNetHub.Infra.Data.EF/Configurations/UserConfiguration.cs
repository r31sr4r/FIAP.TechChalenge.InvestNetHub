using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Data.EF.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(user => user.Phone)
            .IsRequired()
            .HasMaxLength(20); 

        builder.Property(user => user.CPF)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(user => user.RG)
            .HasMaxLength(20); 

        builder.Property(user => user.DateOfBirth)
            .IsRequired();

        builder.Property(user => user.Password)
            .HasMaxLength(1000); 

        builder.Property(user => user.IsActive)
            .IsRequired();

        builder.Property(user => user.CreatedAt)
            .IsRequired();

        builder.Property(user => user.AnalysisDate)
            .HasColumnType("datetime(6)");

        builder.Property(user => user.AnalysisStatus)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(user => user.InvestmentPreferences)
            .HasColumnType("longtext");

        builder.Property(user => user.RiskLevel)
            .HasConversion<int>()
            .IsRequired();

        builder.Ignore(user => user.Events);

    }
}
