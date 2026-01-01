using InvestmentBot.Domain.Portfolio.Enums;

namespace InvestmentBot.ExternalServices.Persistence.Enities;

public class Portfolio
{
    public int Id { get; set; }
    public PortfolioType PortfolioType { get; set; }
    public decimal Balance { get; set; }
    public List<Holding> Holdings { get; set; } = [];
}