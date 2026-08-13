using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// Interest rate indexes
/// </summary>
public record KucoinUaInterestRateIndexes
{
    /// <summary>
    /// ["<c>items</c>"] Items
    /// </summary>
    [JsonPropertyName("items")]
    public KucoinUaInterestRateIndex[] Items { get; set; } = [];
    /// <summary>
    /// ["<c>lastId</c>"] Last id
    /// </summary>
    [JsonPropertyName("lastId")]
    public long? LastId { get; set; }
}

/// <summary>
/// Interest rate
/// </summary>
public record KucoinUaInterestRateIndex
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// ["<c>interestRate</c>"] Interest rate
    /// </summary>
    [JsonPropertyName("interestRate")]
    public decimal InterestRate { get; set; }
}

