using InvestmentBot.ExternalServices.Persistence.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentBot.ExternalServices.Persistence.Configurations;

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ToTable("Portfolios");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PortfolioType)
            .IsRequired();

        builder.Property(p => p.Balance)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasMany(p => p.Holdings)
            .WithOne(h => h.Portfolio)
            .HasForeignKey(h => h.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}