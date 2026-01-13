using InvestmentBot.Application.InvestmentAnalyzer.Options.Stocks;
using InvestmentBot.Domain.InvestmentAnalyzer.Models;
using System.Globalization;
using System.IO.Compression;
using System.Text;

namespace InvestmentBot.Application.InvestmentAnalyzer.Utils;

public static class StockFileReader
{
    private static readonly CultureInfo Ci = CultureInfo.InvariantCulture;

    public static List<DailyBar> ReadStockBars(string ticker, StockOptions options, ZipArchive zip, CancellationToken ct)
    {
        string fileName = SetFileName(ticker);

        var entry = zip.Entries.FirstOrDefault(e =>
            e.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));

        if (entry is null)
            return [];

        using var stream = entry.Open();
        using var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);

        return ReadLastBars(reader, options, ct);
    }

    private static List<DailyBar> ReadLastBars(StreamReader reader, StockOptions options, CancellationToken ct)
    {
        if (options.DaysRead <= 0) return [];

        var q = new Queue<DailyBar>(capacity: options.DaysRead);

        while (!reader.EndOfStream)
        {
            ct.ThrowIfCancellationRequested();

            var line = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.StartsWith("<TICKER>", StringComparison.OrdinalIgnoreCase)) continue;

            var p = line.Split(',');

            if (p.Length < 9) continue;
            if (!p[1].Equals("D", StringComparison.OrdinalIgnoreCase)) continue;
            if (!TryParseDate(p[2], out var date)) continue;
            if (!decimal.TryParse(p[4], NumberStyles.Any, Ci, out var open)) continue;
            if (!decimal.TryParse(p[5], NumberStyles.Any, Ci, out var high)) continue;
            if (!decimal.TryParse(p[6], NumberStyles.Any, Ci, out var low)) continue;
            if (!decimal.TryParse(p[7], NumberStyles.Any, Ci, out var close)) continue;
            if (!long.TryParse(p[8], NumberStyles.Any, Ci, out var vol)) continue;

            var bar = new DailyBar
            {
                Date = date,
                Open = open,
                High = high,
                Low = low,
                Close = close,
                Volume = vol
            };

            if (q.Count >= options.DaysRead) q.Dequeue();
            q.Enqueue(bar);
        }

        return [.. q];
    }

    private static bool TryParseDate(string yyyymmdd, out DateOnly date)
    {
        date = default;
        if (yyyymmdd.Length != 8) return false;
        if (!int.TryParse(yyyymmdd.AsSpan(0, 4), out var y)) return false;
        if (!int.TryParse(yyyymmdd.AsSpan(4, 2), out var m)) return false;
        if (!int.TryParse(yyyymmdd.AsSpan(6, 2), out var d)) return false;

        try { date = new DateOnly(y, m, d); return true; }
        catch { return false; }
    }

    private static string SetFileName(string ticker)
    {
        ticker = ticker.Trim().ToLowerInvariant();
        if (!ticker.EndsWith(".us", StringComparison.OrdinalIgnoreCase))
            ticker += ".us";
        ticker += ".txt";
        return ticker;
    }
}