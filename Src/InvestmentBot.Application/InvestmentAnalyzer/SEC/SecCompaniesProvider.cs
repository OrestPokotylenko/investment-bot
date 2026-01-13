using InvestmentBot.Application.InvestmentAnalyzer.Contracts;
using InvestmentBot.Application.InvestmentAnalyzer.Options.Stocks;
using InvestmentBot.Application.InvestmentAnalyzer.Options.StockScoring;
using InvestmentBot.Application.InvestmentAnalyzer.Utils;
using InvestmentBot.Domain.InvestmentAnalyzer.Contracts;
using InvestmentBot.Domain.InvestmentAnalyzer.Models;
using InvestmentBot.ExternalServices.SEC.Services;
using Microsoft.Extensions.Options;
using System.IO.Compression;

namespace InvestmentBot.Application.InvestmentAnalyzer.SEC
{
    public class SecCompaniesProvider(SecService secService, IOptions<StockOptions> stockOptions, IOptions<StockScoringOptions> scoringOptions) : ICompaniesProvider
    {
        private readonly SecService _secService = secService;
        private readonly StockOptions _stockOptions = stockOptions.Value;
        private readonly StockScoringOptions _scoringOptions = scoringOptions.Value;

        private const string zipPath = "C:\\ProgramData\\InvestmentBot\\d_us_txt.zip";

        public async Task<IReadOnlyList<ICompanyDto>> GetCompaniesAsync(CancellationToken ct)
        {
            var tickers = await _secService.GetCompanyTickersAsync(ct);
            var companies = new List<CompanyFundamentals>();

            using var zip = ZipFile.OpenRead(zipPath);

            foreach (var ticker in tickers)
            {
                bool keep = await TickerFilter.IsTickerValid(ticker.Ticker, _stockOptions, zip, ct);

                if (!keep)
                {
                    continue;
                }

                companies.Add(new CompanyFundamentals
                {
                    Id = ticker.CIK.ToString(),
                    Ticker = ticker.Ticker,
                    CompanyName = ticker.CompanyName
                });
            }

            return companies;
        }

        public async Task<List<(ICompanyDto Fundamentals, double Score)>> ScoreCompaniesAsync(IReadOnlyList<ICompanyDto> companies, CancellationToken ct)
        {
            List<(ICompanyDto Fundamentals, double Score)> scoredCompanies = [];
            using var zip = ZipFile.OpenRead(zipPath);

            foreach (var company in companies)
            {
                double score = StockScorer.Score(company.Ticker, zip, _scoringOptions, _stockOptions, ct);
                scoredCompanies.Add((company, score));
            }

            var topCompanies = scoredCompanies
                .OrderByDescending(x => x.Score)
                .Take(_scoringOptions.TakeTopN)
                .ToList();

            return topCompanies;
        }
    }
}