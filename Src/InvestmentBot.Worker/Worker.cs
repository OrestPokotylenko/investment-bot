using InvestmentBot.Application.InvestmentAnalyzer.SEC;

namespace InvestmentBot.Worker
{
    public class Worker(SecCompaniesProvider provider) : BackgroundService
    {
        private readonly SecCompaniesProvider _provider = provider;

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            var companies = await _provider.GetCompaniesAsync(ct);
            int number = 0;

            var topCompanies = await _provider.ScoreCompaniesAsync(companies, ct);

            foreach (var (Fundamentals, Score) in topCompanies)
            {
                number++;
                Console.WriteLine($"{number}. {Fundamentals.Ticker} - {Fundamentals.CompanyName} - Score: {Score}");
            }
        }
    }
}