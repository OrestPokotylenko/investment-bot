using InvestmentBot.Domain.InvestmentAnalyzer.Enums;
using System.ComponentModel.DataAnnotations;

namespace InvestmentBot.Application.InvestmentAnalyzer.Options.StockScoring;

public sealed class StockScoringOptions
{
    [Required, Range(10, 5000)]
    public int TakeTopN { get; set; }

    [Required, Range(0, 1)]
    public double WeightMomentum { get; set; }

    [Required, Range(0, 1)]
    public double WeightVolatilityPenalty { get; set; }

    [Required, Range(0, 1)]
    public double WeightLiquidity { get; set; }

    [Required, Range(2, 252)]
    public int MomentumLookbackDays { get; set; }

    [Required, Range(1, 200)]
    public double ReturnCapPercent { get; set; }

    [Required, Range(0.1, 200)]
    public double VolatilityCapPercent { get; set; }

    [Required]
    public NormalizationType Normalization { get; set; }
}