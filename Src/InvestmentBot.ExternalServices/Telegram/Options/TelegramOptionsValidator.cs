using Microsoft.Extensions.Options;

namespace InvestmentBot.ExternalServices.Telegram.Options;

[OptionsValidator]
public partial class TelegramOptionsValidator : IValidateOptions<TelegramOptions>
{
}