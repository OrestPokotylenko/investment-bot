using Microsoft.Extensions.Options;

namespace InvestmentBot.Application.InvestmentAnalyzer.Options.StockScoring;

[OptionsValidator]
public partial class StockScoringOptionsValidator : IValidateOptions<StockScoringOptions>
{
}