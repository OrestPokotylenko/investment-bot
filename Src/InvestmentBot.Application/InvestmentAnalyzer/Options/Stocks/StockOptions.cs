using System.ComponentModel.DataAnnotations;

namespace InvestmentBot.Application.InvestmentAnalyzer.Options.Stocks;

public sealed class StockOptions
{
    [Required]
    public required int MinPrice { get; set; }

    [Required]
    public required int MinVolume { get; set; }

    [Required]
    public required int AvarageVolumeMonth { get; set; }

    [Required]
    public required int MaxDailyRangePercent { get; set; }

    [Required]
    public required int DaysRead { get; set; }

    [Required]
    public required int MinDropDays { get; set; }
}