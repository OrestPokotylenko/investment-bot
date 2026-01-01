using InvestmentBot.ExternalServices.Telegram.Options;
using InvestmentBot.ExternalServices.Telegram.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Telegram.Bot;

namespace InvestmentBot.ExternalServices.Telegram.DI;

public static class TelegramModule
{
    public static IServiceCollection AddTelegramModule(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddOptions<TelegramOptions>()
            .Bind(cfg.GetSection(nameof(TelegramOptions)))
            .ValidateOnStart();

        services.AddHttpClient("TelegramBotClient")
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = 3;
                options.Retry.BackoffType = DelayBackoffType.Exponential;
                options.Retry.Delay = TimeSpan.FromSeconds(2);
            });

        services.AddSingleton<TelegramBotService>();

        services.AddSingleton<ITelegramBotClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<TelegramOptions>>().Value;

            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("TelegramBotClient");

            return new TelegramBotClient(options.Token, httpClient);
        });

        return services;
    }
}