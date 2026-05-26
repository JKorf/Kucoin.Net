using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// API key info
/// </summary>
public record KucoinUaApiKeyInfo
{
    /// <summary>
    /// ["<c>remark</c>"] Remark
    /// </summary>
    [JsonPropertyName("remark")]
    public string? Remark { get; set; }
    /// <summary>
    /// ["<c>apiKey</c>"] Api key
    /// </summary>
    [JsonPropertyName("apiKey")]
    public string ApiKey { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>apiVersion</c>"] Api version
    /// </summary>
    [JsonPropertyName("apiVersion")]
    public int ApiVersion { get; set; }
    /// <summary>
    /// ["<c>permission</c>"] Permission
    /// </summary>
    [JsonPropertyName("permission")]
    public string Permission { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>createdAt</c>"] Create time
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// ["<c>uid</c>"] Uid
    /// </summary>
    [JsonPropertyName("uid")]
    public long Uid { get; set; }
    /// <summary>
    /// ["<c>isMaster</c>"] Is master
    /// </summary>
    [JsonPropertyName("isMaster")]
    public bool IsMaster { get; set; }
    /// <summary>
    /// ["<c>region</c>"] Region
    /// </summary>
    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>kycStatus</c>"] Kyc validated
    /// </summary>
    [JsonPropertyName("kycStatus")]
    public bool KycStatus { get; set; }
    /// <summary>
    /// ["<c>siteType</c>"] Site type
    /// </summary>
    [JsonPropertyName("siteType")]
    public string SiteType { get; set; } = string.Empty;
}

