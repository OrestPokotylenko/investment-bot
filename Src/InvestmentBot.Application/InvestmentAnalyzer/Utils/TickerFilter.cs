using InvestmentBot.Application.InvestmentAnalyzer.Options.Stocks;
using InvestmentBot.Domain.InvestmentAnalyzer.Models;
using System.IO.Compression;

namespace InvestmentBot.Application.InvestmentAnalyzer.Utils;

public static class TickerFilter
{
    public static async Task<bool> IsTickerValid(string ticker, StockOptions options, ZipArchive zip, CancellationToken ct)
    {
        var bars = StockFileReader.ReadStockBars(ticker, options, zip, ct);

        if (bars.Count < options.MinDropDays) return false;

        return PassesOptions(bars, options, ticker);
    }

    private static bool PassesOptions(List<DailyBar> bars, StockOptions opt, string ticker)
    {
        var last = bars[^1];

        if (last.Close < opt.MinPrice) { return false; }
        if (last.Volume < opt.MinVolume) { return false; }

        double avgVol = 0;

        for (int i = 0; i < bars.Count; i++)
        {
            avgVol += bars[i].Volume;
        }

        avgVol /= bars.Count;

        if (avgVol < opt.AvarageVolumeMonth) { return false; }

        double maxRangePct = 0;

        for (int i = 0; i < bars.Count; i++)
        {
            var b = bars[i];

            if (b.Close <= 0) { return false; }

            var rangePct = (double)((b.High - b.Low) / b.Close) * 100.0;

            if (rangePct > maxRangePct)
            {
                maxRangePct = rangePct;
            }
        }

        if (maxRangePct > opt.MaxDailyRangePercent) { return false; }

        return true;
    }
}