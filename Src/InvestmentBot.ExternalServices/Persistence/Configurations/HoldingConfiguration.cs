using InvestmentBot.ExternalServices.Persistence.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentBot.ExternalServices.Persistence.Configurations;

public class HoldingConfiguration : IEntityTypeConfiguration<Holding>
{
    public void Configure(EntityTypeBuilder<Holding> builder)
    {
        builder.ToTable("Holdings");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(h => h.Ticker)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(h => h.Quantity)
            .IsRequired();

        builder.Property(h => h.TotalSpent)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(h => h.AcquiredDate)
            .IsRequired();

        builder.HasOne(h => h.Portfolio)
            .WithMany(p => p.Holdings)
            .HasForeignKey(h => h.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}