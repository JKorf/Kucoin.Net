using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// Leverage
/// </summary>
public record KucoinUaLeverage
{
    /// <summary>
    /// ["<c>currency</c>"] Asset
    /// </summary>
    [JsonPropertyName("currency")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>leverage</c>"] Leverage
    /// </summary>
    [JsonPropertyName("leverage")]
    public decimal Leverage { get; set; }
}

