using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// Withdrawal history
/// </summary>
public record KucoinUaWithdrawalHistory
{
    /// <summary>
    /// ["<c>items</c>"] Items
    /// </summary>
    [JsonPropertyName("items")]
    public KucoinUaWithdrawal[] Items { get; set; } = [];
    /// <summary>
    /// ["<c>currentPage</c>"] Current page
    /// </summary>
    [JsonPropertyName("currentPage")]
    public int CurrentPage { get; set; }
    /// <summary>
    /// ["<c>pageSize</c>"] Page size
    /// </summary>
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }
    /// <summary>
    /// ["<c>totalNum</c>"] Total results
    /// </summary>
    [JsonPropertyName("totalNum")]
    public int TotalResults { get; set; }
    /// <summary>
    /// ["<c>totalPage</c>"] Total pages
    /// </summary>
    [JsonPropertyName("totalPage")]
    public int TotalPages { get; set; }
}

/// <summary>
/// Withdrawal info
/// </summary>
public record KucoinUaWithdrawal
{
    /// <summary>
    /// ["<c>id</c>"] Id
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>currency</c>"] Asset
    /// </summary>
    [JsonPropertyName("currency")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>chain</c>"] Network
    /// </summary>
    [JsonPropertyName("chain")]
    public string Network { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>status</c>"] Status
    /// </summary>
    [JsonPropertyName("status")]
    public DepositStatus Status { get; set; }
    /// <summary>
    /// ["<c>address</c>"] Address
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>memo</c>"] Memo
    /// </summary>
    [JsonPropertyName("memo")]
    public string? Memo { get; set; }
    /// <summary>
    /// ["<c>isInner</c>"] Is inner transfer
    /// </summary>
    [JsonPropertyName("isInner")]
    public bool IsInner { get; set; }
    /// <summary>
    /// ["<c>amount</c>"] Deposit quantity
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>fee</c>"] Fee
    /// </summary>
    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }
    /// <summary>
    /// ["<c>walletTxId</c>"] Transaction id
    /// </summary>
    [JsonPropertyName("walletTxId")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// ["<c>createdAt</c>"] Create time
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// ["<c>updatedAt</c>"] Update time
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTime UpdateTime { get; set; }
    /// <summary>
    /// ["<c>remark</c>"] Remark
    /// </summary>
    [JsonPropertyName("remark")]
    public string? Remark { get; set; }
    /// <summary>
    /// ["<c>subStatus</c>"] Status description
    /// </summary>
    [JsonPropertyName("subStatus")]
    public string StatusDescription { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>failureReason</c>"] Failure reason
    /// </summary>
    [JsonPropertyName("failureReason")]
    public string? FailReason { get; set; }
    /// <summary>
    /// ["<c>failureReasonMsg</c>"] Failure reason message
    /// </summary>
    [JsonPropertyName("failureReasonMsg")]
    public string? FailReasonMessage { get; set; }


}

