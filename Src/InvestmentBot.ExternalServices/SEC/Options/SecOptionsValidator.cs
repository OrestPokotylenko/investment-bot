using Microsoft.Extensions.Options;

namespace InvestmentBot.ExternalServices.SEC.Options;

[OptionsValidator]
public partial class SecOptionsValidator : IValidateOptions<SecOptions>
{
}