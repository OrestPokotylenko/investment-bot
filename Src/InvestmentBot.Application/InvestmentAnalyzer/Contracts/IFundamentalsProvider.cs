using InvestmentBot.Domain.InvestmentAnalyzer.Models;

namespace InvestmentBot.Application.InvestmentAnalyzer.Contracts;

public interface IFundamentalsProvider
{
    Task<CompanyFundamentals> GetFundamentalsAsync(string id, CancellationToken ct);
}