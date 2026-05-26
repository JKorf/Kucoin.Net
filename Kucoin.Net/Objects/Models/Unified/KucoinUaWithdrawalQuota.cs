using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// Withdrawal quotas
/// </summary>
public record KucoinUaWithdrawalQuota
{
    /// <summary>
    /// ["<c>currency</c>"] Asset
    /// </summary>
    [JsonPropertyName("currency")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>limitBTCAmount</c>"] Limit BTC quantity
    /// </summary>
    [JsonPropertyName("limitBTCAmount")]
    public decimal LimitBtcQuantity { get; set; }
    /// <summary>
    /// ["<c>usedBTCAmount</c>"] Used BTC quantity
    /// </summary>
    [JsonPropertyName("usedBTCAmount")]
    public decimal UsedBtcQuantity { get; set; }
    /// <summary>
    /// ["<c>quotaCurrency</c>"] Quota asset
    /// </summary>
    [JsonPropertyName("quotaCurrency")]
    public string QuotaAsset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>limitQuotaCurrencyAmount</c>"] Limit quota asset quantity
    /// </summary>
    [JsonPropertyName("limitQuotaCurrencyAmount")]
    public decimal LimitQuotaAssetQuantity { get; set; }
    /// <summary>
    /// ["<c>usedQuotaCurrencyAmount</c>"] Used quota asset quantity
    /// </summary>
    [JsonPropertyName("usedQuotaCurrencyAmount")]
    public decimal UsedQuotaAssetQuantity { get; set; }
    /// <summary>
    /// ["<c>remainAmount</c>"] Remain quantity
    /// </summary>
    [JsonPropertyName("remainAmount")]
    public decimal RemainQuantity { get; set; }
    /// <summary>
    /// ["<c>availableAmount</c>"] Available quantity
    /// </summary>
    [JsonPropertyName("availableAmount")]
    public decimal AvailableQuantity { get; set; }
    /// <summary>
    /// ["<c>withdrawMinFee</c>"] Withdraw min fee
    /// </summary>
    [JsonPropertyName("withdrawMinFee")]
    public decimal WithdrawMinFee { get; set; }
    /// <summary>
    /// ["<c>innerWithdrawMinFee</c>"] Inner withdraw min fee
    /// </summary>
    [JsonPropertyName("innerWithdrawMinFee")]
    public decimal InnerWithdrawMinFee { get; set; }
    /// <summary>
    /// ["<c>withdrawMinSize</c>"] Withdraw min quantity
    /// </summary>
    [JsonPropertyName("withdrawMinSize")]
    public decimal WithdrawMinQuantity { get; set; }
    /// <summary>
    /// ["<c>isWithdrawEnabled</c>"] Is withdraw enabled
    /// </summary>
    [JsonPropertyName("isWithdrawEnabled")]
    public bool IsWithdrawEnabled { get; set; }
    /// <summary>
    /// ["<c>precision</c>"] Precision
    /// </summary>
    [JsonPropertyName("precision")]
    public int Precision { get; set; }
    /// <summary>
    /// ["<c>chainName</c>"] Network name
    /// </summary>
    [JsonPropertyName("chainName")]
    public string NetworkName { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>chainId</c>"] Network id
    /// </summary>
    [JsonPropertyName("chainId")]
    public string NetworkId { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>reason</c>"] Reason
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
    /// <summary>
    /// ["<c>lockedAmount</c>"] Locked quantity
    /// </summary>
    [JsonPropertyName("lockedAmount")]
    public decimal LockedQuantity { get; set; }
}

