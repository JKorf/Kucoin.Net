using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// Leverage setting
/// </summary>
public record KucoinUaLeverageSetting
{
    /// <summary>
    /// ["<c>symbol</c>"] Symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>leverage</c>"] Leverage
    /// </summary>
    [JsonPropertyName("leverage")]
    public decimal Leverage { get; set; }
    /// <summary>
    /// ["<c>marginMode</c>"] Margin mode
    /// </summary>
    [JsonPropertyName("marginMode")]
    public MarginMode MarginMode { get; set; }
}

