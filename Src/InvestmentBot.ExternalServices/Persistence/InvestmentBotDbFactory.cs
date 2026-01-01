using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InvestmentBot.ExternalServices.Persistence;

public class InvestmentBotDbFactory : IDesignTimeDbContextFactory<InvestmentBotDbContext>
{
    public InvestmentBotDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../InvestmentBot.Worker"));
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<InvestmentBotDbContext>();
        var connectionString = configuration.GetConnectionString("InvestmentBotConnection");
        optionsBuilder.UseNpgsql(connectionString);

        return new InvestmentBotDbContext(optionsBuilder.Options);
    }
}