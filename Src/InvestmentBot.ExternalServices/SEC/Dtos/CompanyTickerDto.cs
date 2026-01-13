using System.Text.Json.Serialization;

namespace InvestmentBot.ExternalServices.SEC.Dtos;

public record CompanyTickerDto
{
    [JsonPropertyName("cik_str")]
    public int CIK { get; set; }

    [JsonPropertyName("ticker")]
    public required string Ticker { get; set; }

    [JsonPropertyName("title")]
    public required string CompanyName { get; set; }
}