using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// KYC region
/// </summary>
public record KucoinUaKYCRegion
{
    /// <summary>
    /// ["<c>code</c>"] Code
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>enName</c>"] Name
    /// </summary>
    [JsonPropertyName("enName")]
    public string Name { get; set; } = string.Empty;
}

