namespace InvestmentBot.ExternalServices.Persistence.Enities;

public class Holding
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Ticker { get; set; }
    public int Quantity { get; set; }
    public decimal TotalSpent { get; set; }
    public DateTimeOffset AcquiredDate { get; set; }

    public int PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }

    private Holding()
    {
        AcquiredDate = DateTimeOffset.UtcNow;
    }
}