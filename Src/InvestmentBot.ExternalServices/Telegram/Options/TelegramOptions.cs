using System.ComponentModel.DataAnnotations;

namespace InvestmentBot.ExternalServices.Telegram.Options;

public sealed class TelegramOptions
{
    [Required]
    public required string Token { get; set; }

    [Required]
    public required int ChatId { get; set; }
}