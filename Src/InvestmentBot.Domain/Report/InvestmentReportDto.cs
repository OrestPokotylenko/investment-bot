using InvestmentBot.Domain.Report.Enums;

namespace InvestmentBot.Domain.Report;

public record InvestmentReportDto
{
    public MarketRegimeType MarketRegime { get; set; }
    public required string PortfolioRiskSummary { get; set; }
    public List<StockDecisionDto> Decisions { get; set; } = [];
    public string? Notes { get; set; }
    public DateTime ReportDate { get; set; }
}