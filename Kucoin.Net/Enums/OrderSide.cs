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
        /// Buy order
        /// </summary>
        [Map("buy")]
        Buy,
        /// <summary>
        /// Sell order
        /// </summary>
        [Map("sell")]
        Sell
    }
}
