using System.ComponentModel.DataAnnotations;

namespace InvestmentBot.ExternalServices.SEC.Options;

public sealed class SecOptions
{
    [Required]
    public required string TickersListUrl { get; set; }

    [Required]
    public required string FundamentalsUrl { get; set; }

    [Required]
    public required string SubmissionUrl { get; set; }

    [Required]
    public required string UserAgent { get; set; }
}