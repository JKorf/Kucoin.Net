using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Order filter
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderFilter>))]
    public enum OrderFilter
    {
        /// <summary>
        /// Normal
        /// </summary>
        [Map("NORMAL")]
        Normal,
        /// <summary>
        /// Advanced
        /// </summary>
        [Map("ADVANCED")]
        Advanced
    }
}
