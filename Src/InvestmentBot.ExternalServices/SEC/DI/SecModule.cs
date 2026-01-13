using InvestmentBot.ExternalServices.SEC.Options;
using InvestmentBot.ExternalServices.SEC.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;

namespace InvestmentBot.ExternalServices.SEC.DI;

public static class SecModule
{
    public static IServiceCollection AddSecModule(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddOptions<SecOptions>()
            .Bind(cfg.GetSection(nameof(SecOptions)))
            .ValidateOnStart();

        services.AddHttpClient<SecService>((sp, http) =>
        {
            var options = sp.GetRequiredService<IOptions<SecOptions>>().Value;
            http.DefaultRequestHeaders.UserAgent.ParseAdd(options.UserAgent);
        })
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = 3;
                options.Retry.BackoffType = DelayBackoffType.Exponential;
                options.Retry.Delay = TimeSpan.FromSeconds(2);
            });

        return services;
    }
}