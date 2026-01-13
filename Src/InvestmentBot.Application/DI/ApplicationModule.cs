using InvestmentBot.Application.InvestmentAnalyzer.Options.Stocks;
using InvestmentBot.Application.InvestmentAnalyzer.Options.StockScoring;
using InvestmentBot.Application.InvestmentAnalyzer.SEC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentBot.Application.DI;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddOptions<StockOptions>()
            .Bind(cfg.GetSection(nameof(StockOptions)))
            .ValidateOnStart();

        services.AddOptions<StockScoringOptions>()
            .Bind(cfg.GetSection(nameof(StockScoringOptions)))
            .ValidateOnStart();

        services.AddSingleton<SecCompaniesProvider>();

        return services;
    }
}