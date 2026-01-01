using InvestmentBot.ExternalServices.Persistence.Configurations;
using InvestmentBot.ExternalServices.Persistence.Enities;
using Microsoft.EntityFrameworkCore;

namespace InvestmentBot.ExternalServices.Persistence;

public class InvestmentBotDbContext : DbContext
{
    public InvestmentBotDbContext(DbContextOptions<InvestmentBotDbContext> options) : base(options) { }

    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Holding> Holdings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
        modelBuilder.ApplyConfiguration(new HoldingConfiguration());
    }
}