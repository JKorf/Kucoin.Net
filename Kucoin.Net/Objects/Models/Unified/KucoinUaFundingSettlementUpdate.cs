using Kucoin.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// Funding settlement update
/// </summary>
public record KucoinUaFundingSettlementUpdate
{
    /// <summary>
    /// ["<c>s</c>"] Symbol
    /// </summary>
    [JsonPropertyName("s")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>mp</c>"] Mark price
    /// </summary>
    [JsonPropertyName("mp")]
    public decimal MarkPrice { get; set; }
    /// <summary>
    /// ["<c>q</c>"] Position size
    /// </summary>
    [JsonPropertyName("q")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>mM</c>"] MarginMode
    /// </summary>
    [JsonPropertyName("mM")]
    public MarginMode MarginMode { get; set; }
    /// <summary>
    /// ["<c>sc</c>"] Settlement asset
    /// </summary>
    [JsonPropertyName("sc")]
    public string SettleAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>ft</c>"] Funding timestamp
    /// </summary>
    [JsonPropertyName("ft")]
    public DateTime FundingTime { get; set; }
    /// <summary>
    /// ["<c>ff</c>"] Funding fee
    /// </summary>
    [JsonPropertyName("ff")]
    public decimal FundingFee { get; set; }
    /// <summary>
    /// ["<c>fr</c>"] Funding rate
    /// </summary>
    [JsonPropertyName("fr")]
    public decimal FundingRate { get; set; }
    /// <summary>
    /// ["<c>pV</c>"] Position value
    /// </summary>
    [JsonPropertyName("pV")]
    public decimal PositionValue { get; set; }
}

