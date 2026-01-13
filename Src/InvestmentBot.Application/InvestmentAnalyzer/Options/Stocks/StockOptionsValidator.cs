using Microsoft.Extensions.Options;

namespace InvestmentBot.Application.InvestmentAnalyzer.Options.Stocks;

[OptionsValidator]
public partial class StockOptionsValidator : IValidateOptions<StockOptions>
{
}