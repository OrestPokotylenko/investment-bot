using InvestmentBot.Application.InvestmentAnalyzer.Options.StockScoring;
using InvestmentBot.Application.InvestmentAnalyzer.Options.Stocks;
using System.IO.Compression;

namespace InvestmentBot.Application.InvestmentAnalyzer.Utils;

public static class StockScorer
{
    public static double Score(string ticker, ZipArchive zip, StockScoringOptions scoringOptions, StockOptions stockOptions, CancellationToken ct)
    {
        var bars = StockFileReader.ReadStockBars(ticker, stockOptions, zip, ct);
        if (bars == null || bars.Count == 0) return 0;

        var lookbackDays = Math.Min(scoringOptions.MomentumLookbackDays, bars.Count);
        var recentBars = bars.TakeLast(lookbackDays).ToList();

        var start = recentBars[0].Close;
        var end = recentBars[^1].Close;
        var momentum = (double)((end - start) / start) * 100.0;
        momentum = Math.Max(-scoringOptions.ReturnCapPercent, Math.Min(momentum, scoringOptions.ReturnCapPercent));

        double normMomentum = (momentum + scoringOptions.ReturnCapPercent) / (2 * scoringOptions.ReturnCapPercent);

        double avgVolatility = recentBars
            .Select(b => (double)((b.High - b.Low) / b.Close) * 100.0)
            .DefaultIfEmpty(0).Average();
        avgVolatility = Math.Min(avgVolatility, scoringOptions.VolatilityCapPercent);

        double normVolatility = avgVolatility / scoringOptions.VolatilityCapPercent;

        double avgVolume = recentBars.Select(b => (double)b.Volume).DefaultIfEmpty(0).Average();
        double normLiquidity = Math.Log10(avgVolume + 1) / 7.0;

        double score =
            normMomentum * scoringOptions.WeightMomentum
            - normVolatility * scoringOptions.WeightVolatilityPenalty
            + normLiquidity * scoringOptions.WeightLiquidity;

        return Math.Max(0, Math.Min(100, score * 100));
    }
}