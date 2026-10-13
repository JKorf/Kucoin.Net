using System;
using System.Text.Json.Serialization;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models;

/// <summary>
/// Deposit history
/// </summary>
public record KucoinUaDepositHistory
{
    /// <summary>
    /// ["<c>items</c>"] Items
    /// </summary>
    [JsonPropertyName("items")]
    public KucoinUaDeposit[] Items { get; set; } = [];
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
/// Deposit info
/// </summary>
public record KucoinUaDeposit
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
    /// ["<c>arrears</c>"] Whether an on-chain rollback may cause account debt.
    /// </summary>
    [JsonPropertyName("arrears")]
    public bool Arrears { get; set; }
    /// <summary>
    /// ["<c>url</c>"] Url
    /// </summary>
    [JsonPropertyName("url")]
    public string? ExplorerUrl { get; set; }
    /// <summary>
    /// ["<c>statusRemark</c>"] Status description
    /// </summary>
    [JsonPropertyName("statusRemark")]
    public string StatusDescription { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>preConfirms</c>"] Number of pre-confirmations required for early crediting.
    /// </summary>
    [JsonPropertyName("preConfirms")]
    public int PreConfirms { get; set; }
    /// <summary>
    /// ["<c>confirms</c>"] Number of confirmations required for final crediting.
    /// </summary>
    [JsonPropertyName("confirms")]
    public int Confirms { get; set; }
    /// <summary>
    /// ["<c>currentConfirms</c>"] Confirmations
    /// </summary>
    [JsonPropertyName("currentConfirms")]
    public int CurrentConfirms { get; set; }


}

