using InvestmentBot.Domain.Report.Enums;

namespace InvestmentBot.Domain.Report;

public record StockDecisionDto
{
    public required string Ticker { get; set; }
    public required string CompanyName { get; set; }

    public ActionType Action { get; set; }

    public required decimal CurrentPrice { get; set; }
    public required decimal StopPrice { get; set; }

    public decimal PositionSize { get; set; }
    public bool IsNewPosition { get; set; }
    public int? HoldingDays { get; set; } 

    public string[] Factors { get; set; } = [];
    public required string Rationale { get; set; }
}