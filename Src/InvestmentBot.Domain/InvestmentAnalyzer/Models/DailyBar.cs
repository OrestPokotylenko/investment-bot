namespace InvestmentBot.Domain.InvestmentAnalyzer.Models;

public record DailyBar
{
    public required DateOnly Date { get; init; }
    public required decimal Open { get; init; }
    public required decimal High { get; init; }
    public required decimal Low { get; init; }
    public required decimal Close { get; init; }
    public required long Volume { get; init; }
}