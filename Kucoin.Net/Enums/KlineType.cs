using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Kline type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KlineType>))]
    public enum KlineType
    {
        /// <summary>
        /// Last price
        /// </summary>
        LastPrice,
        /// <summary>
        /// Index price
        /// </summary>
        [Map("index-price")]
        IndexPrice,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("mark-price")]
        MarkPrice,
        /// <summary>
        /// Premium index
        /// </summary>
        [Map("premium-index")]
        PremiumIndex,
    }
}
