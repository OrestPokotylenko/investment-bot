using System.Text;
using InvestmentBot.Domain.Report;
using InvestmentBot.Domain.Report.Enums;

namespace InvestmentBot.Application.Telegram;

public static class TelegramHtmlFormatter
{
    public static string Format(InvestmentReportDto report)
    {
        var sb = new StringBuilder();

        sb.AppendLine("📊 <b>Investment Report</b>");
        sb.AppendLine($"🗓 {report.ReportDate:yyyy-MM-dd}");
        sb.AppendLine();

        sb.AppendLine($"🌍 <b>Market Regime:</b> {FormatRegime(report.MarketRegime)}");
        sb.AppendLine();

        sb.AppendLine("🛡 <b>Portfolio Risk</b>");
        sb.AppendLine(Html(report.PortfolioRiskSummary));
        sb.AppendLine();

        if (!string.IsNullOrWhiteSpace(report.Notes))
        {
            sb.AppendLine("📝 <b>Notes</b>");
            sb.AppendLine(Html(report.Notes));
            sb.AppendLine();
        }

        sb.AppendLine($"📌 <b>Decisions ({report.Decisions.Count})</b>");
        sb.AppendLine();

        foreach (var d in report.Decisions)
        {
            sb.AppendLine(FormatDecision(d));
            sb.AppendLine("────────────");
        }

        sb.AppendLine();

        return sb.ToString();
    }

    private static string FormatDecision(StockDecisionDto d)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"<b>{Html(d.Ticker)}</b> — {Html(d.CompanyName)}");
        sb.AppendLine($"Action: <b>{FormatAction(d.Action)}</b> " +
                      $"({(d.IsNewPosition ? "NEW" : $"HOLDING {d.HoldingDays}d")})");

        sb.AppendLine($"Price: {d.CurrentPrice:0.##}");


        var riskPct = (d.StopPrice - d.CurrentPrice) / d.CurrentPrice * 100;
        sb.AppendLine($"Stop: {d.StopPrice:0.##} ({riskPct:0.#}%)");

        sb.AppendLine($"Position size: {d.PositionSize:0.##}%");

        if (d.Factors.Length > 0)
        {
            sb.AppendLine();
            sb.AppendLine("<b>Factors</b>");
            foreach (var f in d.Factors)
                sb.AppendLine($"• {Html(f)}");
        }

        sb.AppendLine();
        sb.AppendLine($"<i>{Html(d.Rationale)}</i>");

        return sb.ToString();
    }

    private static string FormatAction(ActionType action) =>
        action switch
        {
            ActionType.BUY => "🟢 BUY",
            ActionType.HOLD => "🟡 HOLD",
            ActionType.SELL => "🔴 SELL",
            _ => action.ToString().ToUpperInvariant()
        };

    private static string FormatRegime(MarketRegimeType regime) =>
        regime switch
        {
            MarketRegimeType.BULL => "🟢 BULL MARKET",
            MarketRegimeType.BEAR => "🔴 BEAR MARKET",
            MarketRegimeType.SIDEWAYS => "🟡 SIDEWAYS",
            MarketRegimeType.VOLATILE => "🟠 HIGH VOLATILITY",
            MarketRegimeType.UNKNOWN => "⚪ UNKNOWN",
            _ => Html(regime.ToString().ToUpperInvariant())
        };

    private static string Html(string s) =>
        s.Replace("&", "&amp;")
         .Replace("<", "&lt;")
         .Replace(">", "&gt;");
}