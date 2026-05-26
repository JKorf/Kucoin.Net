using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// Funding fee history
/// </summary>
public record KucoinUaFundingFeeHistory
{
    /// <summary>
    /// ["<c>items</c>"] Items
    /// </summary>
    [JsonPropertyName("items")]
    public KucoinUaFundingFeeEntry[] Items { get; set; } = [];
    /// <summary>
    /// ["<c>lastId</c>"] Last id
    /// </summary>
    [JsonPropertyName("lastId")]
    public long? LastId { get; set; }
}

/// <summary>
/// Funding fee info
/// </summary>
public record KucoinUaFundingFeeEntry
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>marginMode</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("marginMode")]
    public MarginMode MarginMode { get; set; }
    /// <summary>
    /// ["<c>fundingRate</c>"] Funding rate
    /// </summary>
    [JsonPropertyName("fundingRate")]
    public decimal FundingRate { get; set; }
    /// <summary>
    /// ["<c>markPrice</c>"] Mark price
    /// </summary>
    [JsonPropertyName("markPrice")]
    public decimal MarkPrice { get; set; }
    /// <summary>
    /// ["<c>size</c>"] Quantity
    /// </summary>
    [JsonPropertyName("size")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>positionValue</c>"] Position value
    /// </summary>
    [JsonPropertyName("positionValue")]
    public decimal PositionValue { get; set; }
    /// <summary>
    /// ["<c>fundingFee</c>"] Funding fee
    /// </summary>
    [JsonPropertyName("fundingFee")]
    public decimal FundingFee { get; set; }
    /// <summary>
    /// ["<c>settleCurrency</c>"] Settle asset
    /// </summary>
    [JsonPropertyName("settleCurrency")]
    public string SettleAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>settlementTime</c>"] Settlement time
    /// </summary>
    [JsonPropertyName("settlementTime")]
    public DateTime SettlementTime { get; set; }
}

