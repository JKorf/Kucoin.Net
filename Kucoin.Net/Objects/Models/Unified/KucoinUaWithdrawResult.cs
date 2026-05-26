using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// Withdraw result
/// </summary>
public record KucoinUaWithdrawResult
{
    /// <summary>
    /// ["<c>withdrawalId</c>"] Withdrawal id
    /// </summary>
    [JsonPropertyName("withdrawalId")]
    public string WithdrawalId { get; set; } = string.Empty;
}

