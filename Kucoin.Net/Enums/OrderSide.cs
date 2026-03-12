using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// ["<c>buy</c>"] Buy order
        /// </summary>
        [Map("buy")]
        Buy,
        /// <summary>
        /// ["<c>sell</c>"] Sell order
        /// </summary>
        [Map("sell")]
        Sell
    }
}
