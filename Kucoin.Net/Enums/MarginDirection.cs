using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Kucoin.Net.Enums;

/// <summary>
/// Margin set direction
/// </summary>
[JsonConverter(typeof(EnumConverter<MarginDirection>))]
public enum MarginDirection
{
    /// <summary>
    /// ["<c>DEPOSIT</c>"] Deposit margin
    /// </summary>
    [Map("DEPOSIT")]
    Deposit,
    /// <summary>
    /// ["<c>WITHDRAW</c>"] Withdraw margin
    /// </summary>
    [Map("WITHDRAW")]
    Withdraw,
}
