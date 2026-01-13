using InvestmentBot.Domain.InvestmentAnalyzer.Enums;

namespace InvestmentBot.Domain.InvestmentAnalyzer.Models;

public record FinancialPeriod
{
    public required PeriodType PeriodType { get; init; } = PeriodType.Quarterly;
    public required int FiscalYear { get; set; }
    public DateTime? PeriodStartDate { get; set; }
    public DateTime? PeriodEndDate { get; set; }
    public DateTime? FilingDate { get; set; }

    public required string Currency { get; set; } = "USD";

    // Income Statement
    public decimal? Revenue { get; set; }
    public decimal? GrossProfit { get; set; }
    public decimal? OperatingIncome { get; set; }
    public decimal? NetIncome { get; set; }
    public decimal? EarningsPerShare { get; set; }

    // Balance Sheet
    public decimal? TotalAssets { get; set; }
    public decimal? TotalLiabilities { get; set; }
    public decimal? TotalEquity { get; set; }
    public decimal? CashAndEquivalents { get; set; }
    public decimal? ShortTermDebt { get; set; }
    public decimal? LongTermDebt { get; set; }

    // Cash Flow
    public decimal? OperatingCashFlow { get; set; }
    public decimal? InvestingCashFlow { get; set; }
    public decimal? FinancingCashFlow { get; set; }
    public decimal? FreeCashFlow { get; set; }
}