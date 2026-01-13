using InvestmentBot.ExternalServices.SEC.Dtos;
using InvestmentBot.ExternalServices.SEC.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace InvestmentBot.ExternalServices.SEC.Services;

public class SecService(HttpClient httpClient, IOptions<SecOptions> options)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly SecOptions _options = options.Value;

    public async Task<IList<CompanyTickerDto>> GetCompanyTickersAsync(CancellationToken ct)
    {
        try
        {
            var response = await _httpClient.GetAsync(_options.TickersListUrl, ct);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve company tickers. Status: {response.StatusCode}");
            }

            var dict = await response.Content.ReadFromJsonAsync<Dictionary<string, CompanyTickerDto>>(ct);

            return dict?.Values.ToList() 
                ?? throw new Exception("Received empty or invalid company tickers data.");
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to retrieve company tickers from SEC.", ex);
        }
    }

    public async Task GetCompanyFundamentalsAsync(CompanyTickerDto tickerDto, CancellationToken ct)
    {
        string cik10 = tickerDto.CIK.ToString("D10");
        string requestUrl = _options.FundamentalsUrl.Replace("{id}", cik10);

        var response = await _httpClient.GetAsync(requestUrl, ct);


    }
}