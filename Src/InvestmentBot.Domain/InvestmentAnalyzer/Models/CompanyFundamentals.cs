using InvestmentBot.Domain.InvestmentAnalyzer.Contracts;
using InvestmentBot.Domain.InvestmentAnalyzer.Enums;

namespace InvestmentBot.Domain.InvestmentAnalyzer.Models;

public record CompanyFundamentals : ICompanyDto
{
    public required string Id { get; set; }
    public required string Ticker { get; set; }
    public required string CompanyName { get; set; }
    public string? DefaultCurrency { get; init; } = "USD";
    public ProviderType Provider { get; set; } = ProviderType.SEC;
    public List<FinancialPeriod> Periods { get; set; } = [];
}