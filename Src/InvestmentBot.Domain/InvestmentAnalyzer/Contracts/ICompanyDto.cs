namespace InvestmentBot.Domain.InvestmentAnalyzer.Contracts;

public interface ICompanyDto
{
    public string Id { get; }
    public string Ticker { get; }
    public string CompanyName { get; }
}