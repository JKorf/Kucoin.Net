using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Stop price trigger value
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StopPriceType>))]
    public enum StopPriceType
    {
        /// <summary>
        /// ["<c>TP</c>"] Trigger on the last trade price
        /// </summary>
        [Map("TP")]
        TradePrice,
        /// <summary>
        /// ["<c>MP</c>"] Trigger on mark price
        /// </summary>
        [Map("MP")]
        MarkPrice,
        /// <summary>
        /// ["<c>IP</c>"] Trigger on index price
        /// </summary>
        [Map("IP")]
        IndexPrice
    }
}
