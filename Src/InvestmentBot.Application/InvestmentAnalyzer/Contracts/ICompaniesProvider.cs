using InvestmentBot.Domain.InvestmentAnalyzer.Contracts;

namespace InvestmentBot.Application.InvestmentAnalyzer.Contracts;

public interface ICompaniesProvider
{
    Task<IReadOnlyList<ICompanyDto>> GetCompaniesAsync(CancellationToken ct);
    Task<List<(ICompanyDto Fundamentals, double Score)>> ScoreCompaniesAsync(IReadOnlyList<ICompanyDto> companies, CancellationToken ct);
}