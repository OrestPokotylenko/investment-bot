using InvestmentBot.Domain.Report;
using InvestmentBot.ExternalServices.Telegram.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace InvestmentBot.ExternalServices.Telegram.Services;

public class TelegramBotService(ITelegramBotClient client, IOptions<TelegramOptions> options, ILogger<TelegramBotService> logger)
{
    private readonly ITelegramBotClient _client = client;
    private readonly TelegramOptions _options = options.Value;
    private readonly ILogger<TelegramBotService> _logger = logger;

    public async Task SendMessageAsync(InvestmentReportDto report, CancellationToken ct)
    {
        try
        {
            string message = TelegramHtmlFormatter.Format(report);
            await _client.SendMessage(_options.ChatId, message, ParseMode.Html, cancellationToken: ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send Telegram message to chat.");
        }
    }
}