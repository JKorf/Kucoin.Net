using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// Margin mode results
/// </summary>
public record KucoinMarginModesResults
{
    /// <summary>
    /// ["<c>ts</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("ts")]
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// ["<c>items</c>"] Results
    /// </summary>
    [JsonPropertyName("items")]
    public KucoinMarginModesResult[] Results { get; set; } = [];
}

/// <summary>
/// Margin mode result
/// </summary>
public record KucoinMarginModesResult
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
    /// ["<c>code</c>"] Code
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }
    /// <summary>
    /// ["<c>msg</c>"] Message
    /// </summary>
    [JsonPropertyName("msg")]
    public string Message { get; set; } = string.Empty;
}

